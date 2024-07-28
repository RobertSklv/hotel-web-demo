using HotelWebDemo.Extensions;
using HotelWebDemo.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Services.Indexing;

public abstract class Indexer<TEntity, TIndexEntity> : IIndexer<TEntity, TIndexEntity>
    where TEntity : class, IBaseEntity
    where TIndexEntity : class, IBaseEntity
{
    protected abstract DbSet<TEntity> DbSet { get; }

    protected abstract DbSet<TIndexEntity> IndexedDbSet { get; }

    protected abstract void Init();

    protected abstract void LoadRelated(TEntity entity);

    protected abstract TIndexEntity Process(TEntity entity);

    protected virtual IQueryable<TEntity> GetAll() => DbSet;

    public void Process()
    {
        Init();
        List<TEntity> entities = GetAll().ToList();

        List<TIndexEntity> indexedEntities = DoProcess(entities);

        IndexedDbSet.DeleteAll();
        IndexedDbSet.AddRange(indexedEntities);
    }

    public List<TIndexEntity> DoProcess(List<TEntity> entities)
    {
        List<TIndexEntity> indexedEntities = new();

        foreach (TEntity entity in entities)
        {
            LoadRelated(entity);
            TIndexEntity entityIndex = Process(entity);
            ProcessBaseEntityProperties(entity, entityIndex);
            indexedEntities.Add(entityIndex);
        }

        return indexedEntities;
    }

    public void ProcessInsert(TEntity entity)
    {
        IndexedDbSet.Add(Process(entity));
    }

    public void ProcessUpdate(TEntity entity)
    {
        IndexedDbSet.Update(Process(entity));
    }

    public void ProcessDelete(int id)
    {
        IndexedDbSet
            .Where(e => e.Id == id)
            .Take(1)
            .ExecuteDelete();
    }

    protected void ProcessBaseEntityProperties(TEntity entity, TIndexEntity entityIndex)
    {
        entityIndex.Id = entity.Id;
        entityIndex.UpdatedAt = entity.UpdatedAt;
        entityIndex.CreatedAt = entity.CreatedAt;
    }
}
