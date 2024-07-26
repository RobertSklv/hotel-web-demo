using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public interface ICrudService<TEntity, TIndexedEntity>
    where TEntity : class, IBaseEntity
    where TIndexedEntity : class, IBaseEntity
{
    TEntity? Get(int id);

    Task<int> Upsert(TEntity entity);

    Task<int> Update(TEntity entity);

    Task<int> Delete(int id);

    Task<PaginatedList<TIndexedEntity>> List(string orderBy, string direction, int page, int pageSize, Dictionary<string, TableFilter>? filters);
}

public interface ICrudService<TEntity> : ICrudService<TEntity, TEntity>
    where TEntity : class, IBaseEntity
{

}