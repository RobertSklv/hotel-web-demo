using System.Linq.Expressions;

namespace HotelWebDemo.Services;

public interface IEntityHelperService
{
    string[] SplitHierarchicalPropertyName(string propertyName);

    MemberExpression ParseMemberExpression(ParameterExpression param, params string[] propertyName);

    MemberExpression ParseMemberExpression(ParameterExpression param, string propertyName);
}