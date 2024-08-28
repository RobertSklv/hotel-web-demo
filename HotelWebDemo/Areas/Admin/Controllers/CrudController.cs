using System.Diagnostics.CodeAnalysis;
using HotelWebDemo.Extensions;
using HotelWebDemo.Models;
using HotelWebDemo.Models.Components.Admin.Pages;
using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HotelWebDemo.Areas.Admin.Controllers;

public abstract class CrudController<TEntity, TViewModel> : AdminController
    where TEntity : class, IBaseEntity, new()
    where TViewModel : class, IModel, new()
{
    protected readonly ICrudService<TEntity, TViewModel> service;
    protected readonly IAdminPageService adminPageService;
    protected readonly Serilog.ILogger logger;

    protected string? ListingTitle { get; set; }

    protected string OldModelTempDataKey = $"OldModel-{typeof(TViewModel).Name}";

    protected virtual string DefaultCreateViewName { get; } = "Upsert";
    protected virtual string DefaultUpdateViewName { get; } = "Upsert";

    public CrudController(ICrudService<TEntity, TViewModel> service, IAdminPageService adminPageService, Serilog.ILogger logger)
    {
        this.service = service;
        this.adminPageService = adminPageService;
        this.logger = logger;
    }

    public virtual async Task UpsertMethod(TViewModel model) => await service.Upsert(model);

    protected virtual Task<string> MassAction(string massAction, List<int> selectedItemIds) => Task.FromResult("Index");

    [HttpGet]
    public virtual async Task<IActionResult> Index([FromQuery] ListingModel listingModel)
    {
        AddCreateAction();

        if (ListingTitle != null)
        {
            ViewData["Title"] = ListingTitle;
            ViewData["PageHeading"] = ListingTitle;
        }

        return View(await service.CreateListingModel(listingModel));
    }

    [HttpGet]
    public virtual async Task<IActionResult> View(int id)
    {
        TViewModel? viewModel = await GetViewModel(id);
        if (viewModel != null)
        {
            AddBackAction();

            return View(viewModel);
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public virtual async Task<IActionResult> Create()
    {
        AddBackAction();

        return View(DefaultCreateViewName, TempData.Pop<TViewModel>(OldModelTempDataKey) ?? new TViewModel());
    }

    [HttpGet]
    public virtual async Task<IActionResult> Edit(int id)
    {
        TViewModel? viewModel = await GetViewModel(id);
        if (viewModel != null)
        {
            AddBackAction();

            return View(DefaultUpdateViewName, TempData.Pop<TViewModel>(OldModelTempDataKey) ?? viewModel);
        }

        AddMessage($"Entity {typeof(TEntity).Name} with ID {id} doesn't exist.", ColorClass.Danger, log: true);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public virtual async Task<IActionResult> Create(TViewModel model)
    {
        if (!ModelState.IsValid)
        {
            string errors = string.Join(", ", GetModelStateErrors());
            AddMessage("The provided data is not valid, please resolve all validation errors before submitting the form. Errors: " + errors, ColorClass.Danger);

            TempData.Set(OldModelTempDataKey, model);

            return RedirectToAction("Create");
        }

        try
        {
            await UpsertMethod(model);
        }
        catch (Exception e)
        {
            logger.Error(e, $"An error occurred while creating entity {model.GetType().ShortDisplayName()}");
            AddMessage("An error has occured.", ColorClass.Danger);

            TempData.Set(OldModelTempDataKey, model);

            return RedirectToAction("Create");
        }

        logger.Information($"Record successfully created: {model.GetType().ShortDisplayName()}({model.Id})");
        AddMessage("Record successfully created.", ColorClass.Success);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public virtual async Task<IActionResult> Update(TViewModel model)
    {
        if (!ModelState.IsValid)
        {
            string errors = string.Join(", ", GetModelStateErrors());
            AddMessage("The provided data is not valid, please resolve all validation errors before submitting the form. Errors: " + errors, ColorClass.Danger);

            TempData.Set(OldModelTempDataKey, model);

            return RedirectToAction("Edit", new { model.Id });
        }

        try
        {
            await UpsertMethod(model);
        }
        catch (Exception e)
        {
            logger.Error(e, $"An error occurred while saving entity {model.GetType().ShortDisplayName()}({model.Id})");
            AddMessage("An error has occured.", ColorClass.Danger);

            TempData.Set(OldModelTempDataKey, model);

            return RedirectToAction("Edit", new { model.Id });
        }

        logger.Information($"Record successfully saved: {model.GetType().ShortDisplayName()}({model.Id})");
        AddMessage("Record successfully saved.", ColorClass.Success);

        return RedirectToAction("Edit", new { model.Id });
    }

    public virtual async Task<IActionResult> Delete(int id)
    {
        TEntity? entity = await GetEntity(id);

        if (entity != null)
        {
            try
            {
                await service.Delete((int)id);
                logger.Information($"Record successfully deleted: {entity.GetType().ShortDisplayName()}({entity.Id})");
                AddMessage("Record successfully deleted.", ColorClass.Success);
            }
            catch (Exception e)
            {
                logger.Error(e, $"An error occurred while attempting to delete entity {entity.GetType().ShortDisplayName()}({entity.Id})");
                AddMessage("An error has occured.", ColorClass.Danger);
            }
        }
        else
        {
            AddMessage($"Entity {typeof(TEntity).Name} with ID {id} doesn't exist.", ColorClass.Danger, log: true);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Mass(List<int> selectedItemIds, [FromQuery] string massAction, [FromQuery] ListingModel? listingModel = null)
    {
        string backAction = await MassAction(massAction, selectedItemIds);

        return RedirectToAction(backAction, listingModel?.GenerateListingQuery());
    }

    protected virtual async Task<TViewModel?> GetViewModel(int id)
    {
        TEntity? entity = await GetEntity(id);
        if (entity == null)
        {
            AddMessage($"Entity {typeof(TEntity).Name} with ID {id} doesn't exist.", ColorClass.Danger, log: true);

            return null;
        }

        TViewModel? model = service.EntityToViewModel(entity);

        return model;
    }

    protected virtual async Task<TEntity?> GetEntity(int id)
    {
        return await service.Get(id);
    }

    protected List<PageActionButton> GetOrCreatePageActionButtonsList()
    {
        if (ViewData["PageActions"] == null)
        {
            ViewData["PageActions"] = new List<PageActionButton>();
        }

        return (List<PageActionButton>)ViewData["PageActions"]!;
    }

    protected void AddBackAction(
        string action = "Index",
        string? controllerName = null,
        Dictionary<string, object>? requestParameters = null)
    {
        if (controllerName != null)
        {
            GetOrCreatePageActionButtonsList().Add(adminPageService.BackAction(
                area: "Admin",
                controller: controllerName,
                action: action,
                requestParameters: requestParameters));
        }
        else
        {
            GetOrCreatePageActionButtonsList().Add(adminPageService.BackAction(this, action: action));
        }
    }

    protected void AddCreateAction()
    {
        GetOrCreatePageActionButtonsList().Add(adminPageService.CreateAction(this));
    }

    protected void AddMessage(string message, ColorClass color, bool log = false)
    {
        Message msg = new()
        {
            Content = message,
            Color = color
        };

        List<Message> messages = TempData.Get<List<Message>>("Messages") ?? new();
        messages.Add(msg);
        TempData.Set("Messages", messages);

        if (log)
        {
            GetLogMethod(color).Invoke(message);
        }
    }

    private Action<string> GetLogMethod(ColorClass color)
    {
        return color switch
        {
            ColorClass.Info => logger.Information,
            ColorClass.Warning => logger.Warning,
            ColorClass.Danger => logger.Error,
            ColorClass.Primary => logger.Information,
            ColorClass.Secondary => logger.Information,
            ColorClass.Success => logger.Information,
            _ => throw new NotImplementedException(),
        };
    }

    protected List<string> GetModelStateErrors()
    {
        List<string> errors = new();

        foreach (var value in ModelState.Values)
        {
            foreach (var error in value.Errors)
            {
                errors.Add(error.ErrorMessage);
            }
        }

        return errors;
    }
}

public abstract class CrudController<TEntity> : CrudController<TEntity, TEntity>
    where TEntity : class, IBaseEntity, new()
{
    protected CrudController(
        ICrudService<TEntity> service,
        IAdminPageService adminPageService,
        Serilog.ILogger logger)
        : base(service, adminPageService, logger)
    {
    }
}