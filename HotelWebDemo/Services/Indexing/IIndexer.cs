using HotelWebDemo.Models.Database;

namespace HotelWebDemo.Services.Indexing;

public interface IIndexer<TEntity, TIndexEntity>
    where TEntity : class, IBaseEntity
    where TIndexEntity : class, IBaseEntity
{
    TIndexEntity Process(TEntity entity);

    void Process();

    void ProcessInsert(TEntity entity);

    void ProcessUpdate(TEntity entity);

    void ProcessDelete(int id);
}