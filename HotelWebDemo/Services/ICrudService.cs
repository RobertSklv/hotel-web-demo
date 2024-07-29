using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HotelWebDemo.Services;

public interface ICrudService<TEntity, TIndexedEntity> : ICrudRepository<TEntity, TIndexedEntity>
    where TEntity : class, IBaseEntity
    where TIndexedEntity : class, IBaseEntity
{
    Task<PaginatedList<TIndexedEntity>> List(ListingModel<TIndexedEntity> listingModel);

    void InitializeListingModel(ListingModel<TIndexedEntity> listingModel, ViewDataDictionary viewData);

    Table<TIndexedEntity> CreateListingTable(ListingModel<TIndexedEntity> listingModel, PaginatedList<TIndexedEntity> items);

    Task<ListingModel<TIndexedEntity>> CreateListingModel(ViewDataDictionary viewData);
}

public interface ICrudService<TEntity> : ICrudService<TEntity, TEntity>
    where TEntity : class, IBaseEntity
{
}