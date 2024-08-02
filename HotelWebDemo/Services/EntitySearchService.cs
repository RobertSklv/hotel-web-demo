using HotelWebDemo.Models.Attributes;
using HotelWebDemo.Models.Database;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection;

namespace HotelWebDemo.Services;

public class EntitySearchService : IEntitySearchService
{
    private readonly IEntityFilterService filterService;
    private readonly IEntityHelperService helper;

    public EntitySearchService(IEntityFilterService filterService, IEntityHelperService helper)
    {
        this.filterService = filterService;
        this.helper = helper;
    }

    public IQueryable<T> GenerateSearchFilters<T>(IQueryable<T> queryable, string? searchPhrase)
    {
        if (searchPhrase != null)
        {
            Type entityType = typeof(T);
            List<Expression> expressions = new();
            string[] tokens = searchPhrase.Split();
            ParameterExpression param = Expression.Parameter(entityType, "x");

            foreach (PropertyInfo propInfo in GetSearchableProperties(entityType))
            {
                string propertyName = propInfo.Name;

                SelectOptionAttribute? selectOptionAttr = propInfo.PropertyType.GetCustomAttribute<SelectOptionAttribute>();

                if (selectOptionAttr != null && CanPropertyBeMapped(helper.GetHierarchicalProperty(entityType, propertyName + '_' + selectOptionAttr.LabelProperty)))
                {
                    propertyName += '_' + selectOptionAttr.LabelProperty;
                }
                else if (entityType.GetProperty("Name") != null)
                {
                    propertyName += '_' + "Name";
                }

                if (!CanPropertyBeMapped(helper.GetHierarchicalProperty(entityType, propertyName)))
                {
                    continue;
                }

                try
                {
                    MemberExpression property = helper.ParseMemberExpression(param, propertyName);
                    Expression propertyAsString = property.Type == typeof(string)
                        ? property
                        : PropertyToString<T>(property, propInfo.PropertyType);

                    foreach (string token in tokens)
                    {
                        Expression constant = Expression.Constant(token);
                        Expression exprBody = filterService.BuildFilterPredicate<T>(propertyAsString, EntityFilterService.OPERATOR_CONTAINS, constant);
                        expressions.Add(exprBody);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"An error has occured while generating the search filter for property '{propInfo.Name}'.", e);
                }
            }

            queryable = JoinExpressions(queryable, expressions, param);
        }

        return queryable;
    }

    public MethodCallExpression PropertyToString<T>(MemberExpression property, Type propertyType)
    {
        MethodCallExpression castExpression = Expression.Call(
            property,
            propertyType.GetMethod("ToString", BindingFlags.Public | BindingFlags.Instance, Type.EmptyTypes)
                ?? throw new Exception($"Type '{propertyType.ShortDisplayName()}' has no 'ToString' method defined.")
        );

        return castExpression;
    }

    public IQueryable<T> JoinExpressions<T>(IQueryable<T> queryable, List<Expression> expressions, ParameterExpression param)
    {
        if (expressions.Count > 0)
        {
            Expression body = expressions.First();

            for (int i = 1; i < expressions.Count; i++)
            {
                Expression expr = expressions[i];

                body = Expression.OrElse(body, expr);
            }

            Expression<Func<T, bool>> combinedExpression = Expression.Lambda<Func<T, bool>>(body, param);
            queryable = queryable.Where(combinedExpression);
        }

        return queryable;
    }

    public List<PropertyInfo> GetSearchableProperties(Type type)
    {
        List<PropertyInfo> properties = new();

        foreach (PropertyInfo property in type.GetProperties())
        {
            TableColumnAttribute? colAttr = property.GetCustomAttribute<TableColumnAttribute>();

            if (colAttr == null || !colAttr.Searchable)
            {
                continue;
            }

            properties.Add(property);
        }

        return properties;
    }

    public bool CanPropertyBeMapped(PropertyInfo property)
    {
        if (property.GetMethod == null
            || property.GetMethod.IsAbstract
            || !property.GetMethod.IsPublic
            || property.GetMethod.IsStatic)
        {
            return false;
        }
        else if (property.SetMethod == null
            || property.SetMethod.IsAbstract
            || !property.SetMethod.IsPublic
            || property.SetMethod.IsStatic)
        {
            return false;
        }
        else if (property.GetCustomAttribute<NotMappedAttribute>() != null)
        {
            return false;
        }
        else if (!property.PropertyType.IsPrimitive && !(
            property.PropertyType == typeof(string)
            || property.PropertyType == typeof(DateTime)))
        {
            return false;
        }
        else if (property.PropertyType.IsGenericType)
        {
            return false;
        }

        return true;
    }
}
