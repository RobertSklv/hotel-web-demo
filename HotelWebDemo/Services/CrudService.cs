using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HotelWebDemo.Services;

public abstract class CrudService<TEntity, TIndexedEntity> : ICrudService<TEntity, TIndexedEntity>
    where TEntity : class, IBaseEntity
    where TIndexedEntity : class, IBaseEntity
{
    private readonly ICrudService<TEntity, TIndexedEntity> repository;

    public CrudService(ICrudService<TEntity, TIndexedEntity> repository)
    {
        this.repository = repository;
    }

    public async Task<int> Delete(int id)
    {
        return await repository.Delete(id);
    }

    public TEntity? Get(int id)
    {
        return repository.Get(id);
    }

    public async Task<PaginatedList<TIndexedEntity>> List(string orderBy, string direction, int page, int pageSize, Dictionary<string, TableFilter>? filters)
    {
        return await repository.List(orderBy, direction, page, pageSize, filters);
    }

    public async Task<PaginatedList<TIndexedEntity>> List(ListingModel<TIndexedEntity> listingModel)
    {
        return await repository.List(
            listingModel.OrderBy ?? ListingModel.DEFAULT_ORDER_BY,
            listingModel.Direction ?? ListingModel.DEFAULT_DIRECTION,
            listingModel.Page ?? ListingModel.DEFAULT_PAGE,
            listingModel.PageSize,
            listingModel.Filter);
    }

    public void InitializeListingModel(ListingModel<TIndexedEntity> listingModel, ViewDataDictionary viewData)
    {
        listingModel.ActionName = "Index";
        listingModel.OrderBy = (string?)viewData["OrderBy"];
        listingModel.Direction = (string?)viewData["Direction"];
        listingModel.Page = (int?)viewData["Page"];
        listingModel.Filter = (Dictionary<string, TableFilter>?)viewData["Filter"];
    }

    public async Task<int> Update(TEntity entity)
    {
        return await repository.Update(entity);
    }

    public async Task<int> Upsert(TEntity entity)
    {
        return await repository.Upsert(entity);
    }
}

public abstract class CrudService<TEntity> : CrudService<TEntity, TEntity>
    where TEntity : class, IBaseEntity
{
    protected CrudService(ICrudService<TEntity, TEntity> repository)
        : base(repository)
    {
    }
}