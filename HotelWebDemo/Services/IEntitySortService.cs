using HotelWebDemo.Models.Attributes;
using System.Reflection;

namespace HotelWebDemo.Services;

public interface IEntitySortService
{
    List<PropertyInfo> GetOrderableProperties(Type type);

    IQueryable<T> OrderBy<T>(IQueryable<T> queryable, string propertyName, bool descending);
}