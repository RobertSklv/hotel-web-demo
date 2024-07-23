using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;

namespace HotelWebDemo.Services;

public interface ICrudService<TEntity>
    where TEntity : BaseEntity
{
    TEntity? Get(int id);

    Task<int> Upsert(TEntity entity);

    Task<int> Update(TEntity entity);

    Task<int> Delete(int id);

    Task<PaginatedList<TEntity>> List(string orderBy, string direction, int page, int pageSize, Dictionary<string, TableFilter>? filters);
}