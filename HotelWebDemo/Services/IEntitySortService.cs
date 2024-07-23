using HotelWebDemo.Models.Attributes;
using System.Reflection;

namespace HotelWebDemo.Services;

public interface IEntitySortService
{
    List<PropertyInfo> GetOrderableProperties(Type type);

    IOrderedQueryable<T> OrderBy<T>(IQueryable<T> queryable, string propertyName, bool descending);
}