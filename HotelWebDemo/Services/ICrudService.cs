using HotelWebDemo.Data.Repositories;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HotelWebDemo.Services;

public interface ICrudService<TEntity> : ICrudRepository<TEntity>
    where TEntity : class, IBaseEntity
{
    Task<PaginatedList<TEntity>> List(ListingModel<TEntity> listingModel);

    void InitializeListingModel(ListingModel<TEntity> listingModel, ViewDataDictionary viewData);

    Table<TEntity> CreateListingTable(ListingModel<TEntity> listingModel, PaginatedList<TEntity> items);

    Task<ListingModel<TEntity>> CreateListingModel(ViewDataDictionary viewData);
}