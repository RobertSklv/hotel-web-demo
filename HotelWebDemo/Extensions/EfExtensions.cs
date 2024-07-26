using Microsoft.EntityFrameworkCore;

namespace HotelWebDemo.Extensions;

public static class EfExtensions
{
    public static void DeleteAll<TEntity>(this DbSet<TEntity> dbSet)
        where TEntity : class
    {
        foreach (TEntity e in dbSet)
        {
            dbSet.Remove(e);
        }
    }
}
