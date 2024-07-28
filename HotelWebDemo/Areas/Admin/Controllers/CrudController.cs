using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Components.Admin.Pages;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebDemo.Areas.Admin.Controllers;

public abstract class CrudController<TEntity, TIndexedEntity> : AdminController
    where TEntity : class, IBaseEntity, new()
    where TIndexedEntity : class, IBaseEntity
{
    protected readonly ICrudService<TEntity, TIndexedEntity> service;
    protected readonly IAdminPageService adminPageService;

    protected string? ListingTitle { get; set; }

    public CrudController(ICrudService<TEntity, TIndexedEntity> service, IAdminPageService adminPageService)
    {
        this.service = service;
        this.adminPageService = adminPageService;
    }

    [HttpGet]
    public virtual async Task<IActionResult> Index(string orderBy = "Id", string direction = "desc", int page = 1, Dictionary<string, TableFilter>? filters = null)
    {
        ViewData["OrderBy"] = orderBy;
        ViewData["Direction"] = direction;
        ViewData["Page"] = page;
        ViewData["Filter"] = filters;
        AddCreateAction();

        if (ListingTitle != null)
        {
            ViewData["Title"] = ListingTitle;
            ViewData["PageHeading"] = ListingTitle;
        }

        return View(await service.CreateListingModel(ViewData));
    }

    [HttpGet]
    public virtual IActionResult Create()
    {
        TEntity entity = InitializeNew();

        AddBackAction();

        return View("Upsert", entity);
    }

    [HttpGet]
    public virtual IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        TEntity? entity = GetEntity((int)id);

        if (entity == null)
        {
            return NotFound($"Entity with ID {id} doesn't exist.");
        }

        AddBackAction();

        return View("Upsert", entity);
    }

    [HttpPost]
    public virtual async Task<IActionResult> Upsert(TEntity entity)
    {
        bool isCreate = entity.Id == 0;

        await service.Upsert(entity);

        if (isCreate)
        {
            return RedirectToAction("Index");
        }

        return RedirectToAction("Edit", new { entity.Id });
    }

    public virtual async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return RedirectToAction("Index");
        }

        TEntity? entity = GetEntity((int)id);

        if (entity == null)
        {
            return NotFound($"Entity with ID {id} doesn't exist.");
        }

        await service.Delete((int)id);

        return RedirectToAction("Index");
    }

    protected virtual TEntity InitializeNew()
    {
        return new TEntity();
    }

    protected virtual TEntity? GetEntity(int id)
    {
        return service.Get(id);
    }

    protected void AddBackAction()
    {
        ViewData["PageActions"] = new List<PageActionButton>()
        {
            adminPageService.BackAction(this)
        };
    }

    protected void AddCreateAction()
    {
        ViewData["PageActions"] = new List<PageActionButton>()
        {
            adminPageService.CreateAction(this)
        };
    }
}

public abstract class CrudController<TEntity> : CrudController<TEntity, TEntity>
    where TEntity : class, IBaseEntity, new()
{
    protected CrudController(ICrudService<TEntity, TEntity> service, IAdminPageService adminPageService)
        : base(service, adminPageService)
    {
    }
}