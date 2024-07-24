using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HotelWebDemo.Services;

public abstract class CrudService<TEntity> : ICrudService<TEntity>
    where TEntity : BaseEntity
{
    private readonly ICrudService<TEntity> repository;

    public CrudService(ICrudService<TEntity> repository)
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

    public async Task<PaginatedList<TEntity>> List(string orderBy, string direction, int page, int pageSize, Dictionary<string, TableFilter>? filters)
    {
        return await repository.List(orderBy, direction, page, pageSize, filters);
    }

    public async Task<PaginatedList<TEntity>> List(ListingModel<TEntity> listingModel)
    {
        return await repository.List(
            listingModel.OrderBy ?? ListingModel.DEFAULT_ORDER_BY,
            listingModel.Direction ?? ListingModel.DEFAULT_DIRECTION,
            listingModel.Page ?? ListingModel.DEFAULT_PAGE,
            listingModel.PageSize,
            listingModel.Filter);
    }

    public void InitializeListingModel(ListingModel<TEntity> listingModel, ViewDataDictionary viewData)
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
