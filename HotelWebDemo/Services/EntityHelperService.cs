using System.Linq.Expressions;

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
}
