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

    public abstract TViewModel EntityToViewModel(TEntity entity);

    public abstract TEntity ViewModelToEntity(TViewModel model);

    public virtual async Task<int> Delete(int id)
    {
        return await repository.Delete(id);
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

    public virtual async Task<int> Update(TEntity entity)
    {
        return await repository.Update(entity);
    }

    public virtual async Task<int> Upsert(TViewModel model)
    {
        return await repository.Upsert(ViewModelToEntity(model));
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