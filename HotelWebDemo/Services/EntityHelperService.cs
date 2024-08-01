using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HotelWebDemo.Services;

public class EntityHelperService : IEntityHelperService
{
    public string[] SplitHierarchicalPropertyName(string propertyName)
    {
        return propertyName.Split('_');
    }

    public MemberExpression ParseMemberExpression(ParameterExpression param, params string[] propertyName)
    {
        if (propertyName.Length > 1)
        {
            string bottom = propertyName[^1];
            string[] parentHierarchy = new string[propertyName.Length - 1];
            Array.Copy(propertyName, 0, parentHierarchy, 0, parentHierarchy.Length);

            MemberExpression parentHierarchyExpression = ParseMemberExpression(param, parentHierarchy);

            if (string.IsNullOrEmpty(bottom))
            {
                return parentHierarchyExpression;
            }

            return Expression.Property(parentHierarchyExpression, bottom);
        }
        else if (propertyName.Length == 1)
        {
            return Expression.Property(param, propertyName[0]);
        }

        throw new Exception($"Failed to parse an empty property name!");
    }

    public MemberExpression ParseMemberExpression(ParameterExpression param, string propertyName)
    {
        return ParseMemberExpression(param, SplitHierarchicalPropertyName(propertyName));
    }

    public PropertyInfo GetHierarchicalProperty(Type type, params string[] propertyName)
    {
        if (propertyName.Length == 0)
        {
            throw new Exception($"Failed to parse an empty property name!");
        }

        Type currentType = type;
        PropertyInfo? prop = null;

        foreach (string node in propertyName)
        {
            prop = currentType.GetProperty(node);

            if (prop == null)
            {
                throw new Exception($"Type '{currentType.ShortDisplayName()}' has no such property with the name: {node}");
            }

            currentType = prop.PropertyType;
        }

        return prop ?? throw new Exception($"Failed to parse the hierarchical property '{string.Join(".", propertyName)}' of type '{type.ShortDisplayName()}'");
    }

    public PropertyInfo GetHierarchicalProperty(Type type, string propertyName)
    {
        return GetHierarchicalProperty(type, SplitHierarchicalPropertyName(propertyName));
    }

    public PropertyInfo GetHierarchicalProperty(Type type, PropertyInfo property)
    {
        return GetHierarchicalProperty(type, SplitHierarchicalPropertyName(property.Name));
    }
}
