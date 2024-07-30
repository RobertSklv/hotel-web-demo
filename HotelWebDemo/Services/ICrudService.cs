using HotelWebDemo.Models;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HotelWebDemo.Services;

public interface ICrudService<TEntity, TViewModel>
    where TEntity : class, IBaseEntity
    where TViewModel : class, IModel
{
    TEntity? Get(int id);

    List<TEntity> GetAll();

    Task<int> Upsert(TViewModel model);

    Task<int> Update(TEntity entity);

    Task<int> Delete(int id);

    Task<PaginatedList<TEntity>> List(string orderBy, string direction, int page, int pageSize, Dictionary<string, TableFilter>? filters);

    TViewModel EntityToViewModel(TEntity entity);

    TEntity ViewModelToEntity(TViewModel model);

    Task<PaginatedList<TEntity>> List(ListingModel<TEntity> listingModel);

    void InitializeListingModel(ListingModel<TEntity> listingModel, ViewDataDictionary viewData);

    Table<TEntity> CreateListingTable(ListingModel<TEntity> listingModel, PaginatedList<TEntity> items);

    Task<ListingModel<TEntity>> CreateListingModel(ViewDataDictionary viewData);
}

public interface ICrudService<TEntity> : ICrudService<TEntity, TEntity>
    where TEntity : class, IBaseEntity
{
}