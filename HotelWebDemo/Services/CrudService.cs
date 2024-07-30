using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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

    public virtual async Task<PaginatedList<TEntity>> List(string orderBy, string direction, int page, int pageSize, Dictionary<string, TableFilter>? filters)
    {
        return await repository.List(orderBy, direction, page, pageSize, filters);
    }

    public virtual async Task<PaginatedList<TEntity>> List(ListingModel<TEntity> listingModel)
    {
        return await repository.List(
            listingModel.OrderBy ?? ListingModel.DEFAULT_ORDER_BY,
            listingModel.Direction ?? ListingModel.DEFAULT_DIRECTION,
            listingModel.Page ?? ListingModel.DEFAULT_PAGE,
            listingModel.PageSize,
            listingModel.Filter);
    }

    public virtual void InitializeListingModel(ListingModel<TEntity> listingModel, ViewDataDictionary viewData)
    {
        listingModel.ActionName = "Index";
        listingModel.OrderBy = (string?)viewData["OrderBy"];
        listingModel.Direction = (string?)viewData["Direction"];
        listingModel.Page = (int?)viewData["Page"];
        listingModel.Filter = (Dictionary<string, TableFilter>?)viewData["Filter"];
    }

    public virtual Table<TEntity> CreateListingTable(ListingModel<TEntity> listingModel, PaginatedList<TEntity> items)
    {
        return new Table<TEntity>(listingModel, items)
            .SetOrderable(true)
            .SetFilterable(true)
            .AddRowActions()
            .AddPagination(items);
    }

    public virtual async Task<ListingModel<TEntity>> CreateListingModel(ViewDataDictionary viewData)
    {
        ListingModel<TEntity> model = new();
        InitializeListingModel(model, viewData);

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