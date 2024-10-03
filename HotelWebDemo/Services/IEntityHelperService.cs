using System.Linq.Expressions;
using System.Reflection;

namespace HotelWebDemo.Services;

public interface IEntityHelperService
{
    string[] SplitHierarchicalPropertyName(string propertyName);

    MemberExpression ParseMemberExpression(ParameterExpression param, params string[] propertyName);

    MemberExpression ParseMemberExpression(ParameterExpression param, string propertyName);

    PropertyInfo GetHierarchicalProperty(Type type, params string[] propertyName);

    PropertyInfo GetHierarchicalProperty(Type type, string propertyName);

    PropertyInfo GetHierarchicalProperty(Type type, PropertyInfo property);

    bool CanPropertyBeMapped(PropertyInfo property);

    bool CanPropertyBeMapped(Type type, string propertyName);
}