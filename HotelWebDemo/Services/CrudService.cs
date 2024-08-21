using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Components.Common;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public abstract class CrudService<TEntity, TViewModel> : ICrudService<TEntity, TViewModel>
    where TEntity : class, IBaseEntity
    where TViewModel : class, IModel
{
    private readonly ICrudRepository<TEntity> repository;

    public CrudService(ICrudRepository<TEntity> repository)
    {
        this.repository = repository;
    }

    public virtual TViewModel EntityToViewModel(TEntity entity)
    {
        throw new NotImplementedException($"Required method {nameof(EntityToViewModel)} was not overridden.");
    }

    public virtual TEntity ViewModelToEntity(TViewModel model)
    {
        throw new NotImplementedException($"Required method {nameof(ViewModelToEntity)} was not overridden.");
    }

    public virtual async Task<TViewModel> EntityToViewModelAsync(TEntity entity)
    {
        return await Task.FromResult(EntityToViewModel(entity));
    }

    public virtual async Task<TEntity> ViewModelToEntityAsync(TViewModel model)
    {
        return await Task.FromResult(ViewModelToEntity(model));
    }

    public virtual async Task<bool> Delete(int id)
    {
        return await repository.Delete(id) > 0;
    }

    public virtual TEntity? Get(int id)
    {
        return repository.Get(id);
    }

    public virtual List<TEntity> GetAll()
    {
        return repository.GetAll();
    }

    public async Task<List<TEntity>> GetByIds(IEnumerable<int> ids)
    {
        return await repository.GetByIds(ids);
    }

    public virtual async Task<PaginatedList<TEntity>> List(ListingModel listingModel)
    {
        return await repository.List(listingModel);
    }

    public virtual async Task<PaginatedList<TEntity>> List(ListingModel<TEntity> listingModel)
    {
        return await repository.List(listingModel);
    }

    public virtual void InitializeListingModel(ListingModel<TEntity> listingModel, ListingModel listingQuery)
    {
        listingModel.Copy(listingQuery);
    }

    public virtual Table<TEntity> CreateListingTable(ListingModel<TEntity> listingModel, PaginatedList<TEntity> items)
    {
        return new Table<TEntity>(listingModel, items)
            .SetSearchable(true)
            .SetOrderable(true)
            .SetFilterable(true)
            .AddChainCall(CreateEditRowAction)
            .AddChainCall(CreateDeleteRowAction)
            .AddPagination(true);
    }

    public virtual Table<TEntity> CreateEditRowAction(Table<TEntity> table)
    {
        return table.AddRowAction("Edit", RequestMethod.Get, BootstrapIconType.Pencil, CustomizeEditRowAction);
    }

    public virtual Table<TEntity> CreateDeleteRowAction(Table<TEntity> table)
    {
        return table.AddRowAction("Delete", RequestMethod.Delete, BootstrapIconType.TrashCan, CustomizeDeleteRowAction);
    }

    public virtual RowAction CustomizeEditRowAction(RowAction action)
    {
        return action;
    }

    public virtual RowAction CustomizeDeleteRowAction(RowAction action)
    {
        return action
            .SetColor(ColorClass.Danger)
            .SetConfirmationTitle("Delete confirmation")
            .SetConfirmationMessage(item => $"Are you sure you want to delete {item.GetType().Name} with ID {item.Id}?");
    }

    public virtual async Task<ListingModel<TEntity>> CreateListingModel(ListingModel listingQuery)
    {
        ListingModel<TEntity> model = new();
        InitializeListingModel(model, listingQuery);

        PaginatedList<TEntity> items = await List(model);

        model.Table = CreateListingTable(model, items);

        return model;
    }

    public virtual async Task<bool> Update(TEntity entity)
    {
        return await repository.Update(entity) > 0;
    }

    public virtual async Task<bool> Upsert(TViewModel model)
    {
        TEntity entity = await ViewModelToEntityAsync(model);

        return await repository.Upsert(entity) > 0;
    }
}

public abstract class CrudService<TEntity> : CrudService<TEntity, TEntity>
    where TEntity : class, IBaseEntity
{
    protected CrudService(ICrudRepository<TEntity> repository)
        : base(repository)
    {
    }

    public sealed override TEntity ViewModelToEntity(TEntity model)
    {
        return model;
    }

    public sealed override TEntity EntityToViewModel(TEntity entity)
    {
        return entity;
    }
}