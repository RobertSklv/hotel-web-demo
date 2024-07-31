using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models;
using HotelWebDemo.Models.Components.Admin.Tables;
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
        listingModel.ActionName = "Index";
        listingModel.OrderBy = listingQuery?.OrderBy ?? ListingModel.DEFAULT_ORDER_BY;
        listingModel.Direction = listingQuery?.Direction ?? ListingModel.DEFAULT_DIRECTION;
        listingModel.Page = listingQuery?.Page ?? ListingModel.DEFAULT_PAGE;
        listingModel.PageSize = listingQuery?.PageSize ?? ListingModel.DEFAULT_PAGE_SIZE;
        listingModel.Filters = listingQuery?.Filters;
        listingModel.SearchPhrase = listingQuery?.SearchPhrase;
    }

    public virtual Table<TEntity> CreateListingTable(ListingModel<TEntity> listingModel, PaginatedList<TEntity> items)
    {
        return new Table<TEntity>(listingModel, items)
            .SetSearchable(true)
            .SetOrderable(true)
            .SetFilterable(true)
            .AddRowActions()
            .AddPagination(items);
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