using HotelWebDemo.Models.Attributes;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;
using System.Reflection;

namespace HotelWebDemo.Services;

public class EntitySortService : IEntitySortService
{
    private readonly IEntityHelperService helper;

    public EntitySortService(IEntityHelperService helper)
    {
        this.helper = helper;
    }

    public List<PropertyInfo> GetOrderableProperties(Type type)
    {
        List<PropertyInfo> properties = new();

        foreach (PropertyInfo property in type.GetProperties())
        {
            TableColumnAttribute? colAttr = property.GetCustomAttribute<TableColumnAttribute>();

            if (colAttr == null || !colAttr.Orderable)
            {
                continue;
            }

            properties.Add(property);
        }

        return properties;
    }

    public IQueryable<T> OrderBy<T>(IQueryable<T> queryable, string propertyName, bool descending)
    {
        foreach (PropertyInfo prop in GetOrderableProperties(typeof(T)))
        {
            if (prop.Name != propertyName)
            {
                continue;
            }

            try
            {
                return GenerateAndInvokeOrderBy(queryable, propertyName, prop.PropertyType, descending);
            }
            catch (Exception)
            {
                //throw new Exception($"Failed to construct order by clause for the parameter: {propertyName}.", e);

                break;
            }
        }

        throw new Exception($"Failed to order by property: {propertyName}. Entity type: {typeof(T).ShortDisplayName()}");
    }

    public IOrderedQueryable<T> GenerateAndInvokeOrderBy<T>(IQueryable<T> source, string propertyName, Type propertyType, bool descending)
    {
        Type sourceType = typeof(T);
        Type keySelectorType = typeof(Func<,>).MakeGenericType(sourceType, propertyType);
        ParameterExpression param = Expression.Parameter(typeof(T), "x");
        MemberExpression property = helper.ParseMemberExpression(param, propertyName);
        LambdaExpression selector = Expression.Lambda(keySelectorType, property, param);

        MethodInfo method = descending
            ? typeof(Queryable).GetMethods().Single(method =>
                method.Name == "OrderByDescending" &&
                method.GetParameters().Length == 2)
            : typeof(Queryable).GetMethods().Single(method =>
                method.Name == "OrderBy" &&
                method.GetParameters().Length == 2);

        MethodInfo genericMethod = method.MakeGenericMethod(new[] { sourceType, propertyType });

        return (IOrderedQueryable<T>)genericMethod.Invoke(source, new object[] { source, selector })!;
    }
}