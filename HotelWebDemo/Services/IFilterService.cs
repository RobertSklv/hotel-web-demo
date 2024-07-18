using HotelWebDemo.Models.Components;
using System.Linq.Expressions;
using System.Reflection;

namespace HotelWebDemo.Services;

public interface IFilterService
{
    object? ParseValue(Type type, string value);

    List<PropertyInfo> GetFilterableProperties(Type type);

    IQueryable<T> FilterBy<T>(IQueryable<T> queryable, Dictionary<string, TableFilter>? filters);

    Expression<Func<T, bool>> BuildFilterPredicate<T>(string propertyName, string @operator, dynamic value, dynamic? secondaryValue = null);
}