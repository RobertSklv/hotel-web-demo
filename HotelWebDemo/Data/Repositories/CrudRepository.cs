using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public abstract class CrudRepository<TEntity, TIndexedEntity> : ICrudService<TEntity, TIndexedEntity>
    where TEntity : class, IBaseEntity
    where TIndexedEntity : class, IBaseEntity
{
    public abstract DbSet<TEntity> DbSet { get; }

    public abstract DbSet<TIndexedEntity> IndexedDbSet { get; }

    protected readonly AppDbContext db;
    protected readonly IEntityFilterService filterService;
    protected readonly IEntitySortService sortService;

    public CrudRepository(AppDbContext db, IEntityFilterService filterService, IEntitySortService sortService)
    {
        this.db = db;
        this.filterService = filterService;
        this.sortService = sortService;
    }

    public virtual IQueryable<TIndexedEntity> List(DbSet<TIndexedEntity> dbSet)
    {
        return dbSet;
    }

    public virtual TEntity? Get(int id)
    {
        return DbSet.Where(e => e.Id == id).FirstOrDefault();
    }

    public virtual async Task<int> Upsert(TEntity entity)
    {
        if (entity.Id > 0)
        {
            return await Update(entity);
        }

        await DbSet.AddAsync(entity);

        return await db.SaveChangesAsync();
    }

    public virtual async Task<int> Update(TEntity entity)
    {
        DbSet.Update(entity);

        return await db.SaveChangesAsync();
    }

    public virtual async Task<int> Delete(int id)
    {
        DbSet
            .Where(c => c.Id == id)
            .Take(1)
            .ExecuteDelete();

        return await db.SaveChangesAsync();
    }

    public virtual async Task<PaginatedList<TIndexedEntity>> List(string orderBy, string direction, int page, int pageSize, Dictionary<string, TableFilter>? filters)
    {
        IQueryable<TIndexedEntity> entities = List(IndexedDbSet);
        bool desc = direction == "desc";

        entities = sortService.OrderBy(entities, orderBy, desc);
        entities = filterService.FilterBy(entities, filters);

        PaginatedList<TIndexedEntity> paginatedList = await PaginatedList<TIndexedEntity>.CreateAsync(entities, page, pageSize);

        return paginatedList;
    }
}

public abstract class CrudRepository<TEntity> : CrudRepository<TEntity, TEntity>
    where TEntity : class, IBaseEntity
{
    protected CrudRepository(AppDbContext db, IEntityFilterService filterService, IEntitySortService sortService)
        : base(db, filterService, sortService)
    {
    }
}