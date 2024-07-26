using HotelWebDemo.Data;
using HotelWebDemo.Extensions;
using HotelWebDemo.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Services.Indexing;

public abstract class Indexer<TEntity, TIndexEntity> : IIndexer
    where TEntity : class, IBaseEntity
    where TIndexEntity : class, IBaseEntity
{
    protected abstract DbSet<TEntity> DbSet { get; }

    protected abstract DbSet<TIndexEntity> IndexedDbSet { get; }

    protected readonly AppDbContext db;

    public Indexer(AppDbContext db)
    {
        this.db = db;
    }

    protected abstract void Init();

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
            TIndexEntity entityIndex = Process(entity);
            ProcessBaseEntityProperties(entity, entityIndex);
            indexedEntities.Add(entityIndex);
        }

        return indexedEntities;
    }

    protected void ProcessBaseEntityProperties(TEntity entity, TIndexEntity entityIndex)
    {
        entityIndex.Id = entity.Id;
        entityIndex.UpdatedAt = entity.UpdatedAt;
        entityIndex.CreatedAt = entity.CreatedAt;
    }
}
