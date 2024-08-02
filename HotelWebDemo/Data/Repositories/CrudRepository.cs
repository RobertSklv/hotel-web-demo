using HotelWebDemo.Models;
using HotelWebDemo.Models.Components.Admin.Tables;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using HotelWebDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Data.Repositories;

public abstract class CrudRepository<TEntity, TViewModel> : ICrudRepository<TEntity, TViewModel>
    where TEntity : class, IBaseEntity
    where TViewModel : class, IModel
{
    public abstract DbSet<TEntity> DbSet { get; }

    protected readonly AppDbContext db;
    protected readonly IEntityFilterService filterService;
    protected readonly IEntitySortService sortService;
    private readonly IEntitySearchService searchService;

    public CrudRepository(
        AppDbContext db,
        IEntityFilterService filterService,
        IEntitySortService sortService,
        IEntitySearchService searchService)
    {
        this.db = db;
        this.filterService = filterService;
        this.sortService = sortService;
        this.searchService = searchService;
    }

    public virtual IQueryable<TEntity> List(DbSet<TEntity> dbSet)
    {
        return dbSet;
    }

    public virtual TEntity? Get(int id)
    {
        return DbSet.Where(e => e.Id == id).FirstOrDefault();
    }

    public virtual List<TEntity> GetAll()
    {
        return DbSet.ToList();
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
        int deleteResult = await DbSet
            .Where(c => c.Id == id)
            .Take(1)
            .ExecuteDeleteAsync();

        return deleteResult;
    }

    public virtual async Task<PaginatedList<TEntity>> List(ListingModel listingModel)
    {
        if (listingModel.OrderBy == null) throw new ArgumentException($"The {nameof(ListingModel.OrderBy)} value is required.");
        if (listingModel.Direction == null) throw new ArgumentException($"The {nameof(ListingModel.Direction)} value is required.");
        if (listingModel.Page == null) throw new ArgumentException($"The {nameof(ListingModel.Page)} value is required.");

        IQueryable<TEntity> entities = List(DbSet);
        bool desc = listingModel.Direction == ListingModel.DEFAULT_DIRECTION;

        entities = sortService.OrderBy(entities, listingModel.OrderBy, desc);
        entities = filterService.FilterBy(entities, listingModel.Filters);
        entities = searchService.GenerateSearchFilters(entities, listingModel.SearchPhrase);

        PaginatedList<TEntity> paginatedList = await PaginatedList<TEntity>.CreateAsync(
            entities,
            (int)listingModel.Page,
            listingModel.PageSize ?? ListingModel.DEFAULT_PAGE_SIZE);

        return paginatedList;
    }
}

public abstract class CrudRepository<TEntity> : CrudRepository<TEntity, TEntity>
    where TEntity : class, IBaseEntity
{
    protected CrudRepository(
        AppDbContext db,
        IEntityFilterService filterService,
        IEntitySortService sortService,
        IEntitySearchService searchService)
        : base(db, filterService, sortService, searchService)
    {
    }
}