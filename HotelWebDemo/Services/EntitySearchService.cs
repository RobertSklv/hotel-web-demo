using HotelWebDemo.Models.Attributes;
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
            List<Expression<Func<T, bool>>> expressions = new();
            string[] tokens = searchPhrase.Split();

            foreach (PropertyInfo propInfo in GetSearchableProperties(typeof(T)))
            {
                try
                {
                    ParameterExpression param = Expression.Parameter(typeof(T), "x");
                    MemberExpression property = helper.ParseMemberExpression(param, propInfo.Name);
                    Expression propertyAsString = PropertyToString<T>(param, property);

                    foreach (string token in tokens)
                    {
                        Expression constant = Expression.Constant(token);
                        Expression exprBody = filterService.BuildFilterPredicate<T>(propertyAsString, EntityFilterService.OPERATOR_CONTAINS, constant);
                        Expression<Func<T, bool>> expr = Expression.Lambda<Func<T, bool>>(exprBody, param);
                        expressions.Add(expr);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"An error has occured while generating the search filter for property '{propInfo.Name}'.", e);
                }
            }

            queryable = JoinExpressions(queryable, expressions);
        }

        return queryable;
    }

    public MethodCallExpression PropertyToString<T>(ParameterExpression param, MemberExpression property)
    {
        MethodCallExpression castExpression = Expression.Call(
            Expression.Convert(property, typeof(object)),
            typeof(object).GetMethod("ToString")
        );

        return castExpression;
    }

    public IQueryable<T> JoinExpressions<T>(IQueryable<T> queryable, List<Expression<Func<T, bool>>> expressions)
    {
        if (expressions.Count > 0)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "x");
            Expression body = expressions.First();

            for (int i = 1; i < expressions.Count; i++)
            {
                Expression<Func<T, bool>> expr = expressions[i];

                body = Expression.OrElse(
                    Expression.Invoke(body, param),
                    Expression.Invoke(expr, param)
                );
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
}
