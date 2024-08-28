using HotelWebDemo.Models;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public interface ICrudService<TEntity, TViewModel>
    where TEntity : class, IBaseEntity
    where TViewModel : class, IModel
{
    Task<TEntity?> Get(int id);

    Task<List<TEntity>> GetAll();

    Task<List<TEntity>> GetByIds(IEnumerable<int> ids);

    Task<bool> Upsert(TViewModel model);

    Task<bool> Update(TEntity entity);

    Task<bool> Delete(int id);

    Task<PaginatedList<TEntity>> List(ListingModel listingModel);

    TViewModel EntityToViewModel(TEntity entity);

    TEntity ViewModelToEntity(TViewModel model);

    Task<TViewModel> EntityToViewModelAsync(TEntity entity);

    Task<TEntity> ViewModelToEntityAsync(TViewModel model);

    Task<PaginatedList<TEntity>> List(ListingModel<TEntity> listingModel);

    void InitializeListingModel(ListingModel<TEntity> listingModel, ListingModel listingQuery);

    Table<TEntity> CreateListingTable(ListingModel<TEntity> listingModel, PaginatedList<TEntity> items);

    Task<ListingModel<TEntity>> CreateListingModel(ListingModel listingQuery);
}

public interface ICrudService<TEntity> : ICrudService<TEntity, TEntity>
    where TEntity : class, IBaseEntity
{
}