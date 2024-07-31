using System.Linq.Expressions;
using System.Reflection;

namespace HotelWebDemo.Services;

public interface IEntitySearchService
{
    IQueryable<T> GenerateSearchFilters<T>(IQueryable<T> queryable, string? searchPhrase);

    IQueryable<T> JoinExpressions<T>(IQueryable<T> queryable, List<Expression<Func<T, bool>>> expressions);

    List<PropertyInfo> GetSearchableProperties(Type type);
}