using HotelWebDemo.Models;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Data.Repositories;

public interface ICrudRepository<TEntity, TViewModel>
    where TEntity : class, IBaseEntity
    where TViewModel : class, IModel
{
    TEntity? Get(int id);

    List<TEntity> GetAll();

    Task<int> Upsert(TEntity entity);

    Task<int> Update(TEntity entity);

    Task<int> Delete(int id);

    Task<PaginatedList<TEntity>> List(string orderBy, string direction, int page, int pageSize, Dictionary<string, TableFilter>? filters);
}

public interface ICrudRepository<TEntity> : ICrudRepository<TEntity, TEntity>
    where TEntity : class, IBaseEntity
{
}