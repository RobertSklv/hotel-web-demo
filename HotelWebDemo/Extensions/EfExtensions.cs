using System.Linq.Expressions;

namespace HotelWebDemo.Extensions;

public static class EfExtensions
{
    public static IOrderedQueryable<T> OrderByExtended<T, TKey>(this IQueryable<T> queryable, Expression<Func<T, TKey>> keySelector, bool descending)
    {
        return descending
            ? queryable.OrderByDescending(keySelector)
            : queryable.OrderBy(keySelector);
    }
}
