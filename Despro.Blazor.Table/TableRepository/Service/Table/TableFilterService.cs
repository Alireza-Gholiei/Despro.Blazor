using Despro.Blazor.Table.TableRepository.Interface.Table;
using System.Linq.Expressions;

namespace Despro.Blazor.Table.TableRepository.Service.Table
{
    //public class TableFilterService
    //{
        //public Expression<Func<T, bool>> GetFilter<T>(IColumn<T> column, string value)
        //{
        //    var property = column.Property;

        //    if (column.SearchExpression != null)
        //    {
        //        var wrappedExpression = Expression.Invoke(column.SearchExpression, property.Parameters[0], Expression.Constant(value));
        //        return Expression.Lambda<Func<T, bool>>(wrappedExpression, property.Parameters);
        //    }

        //    var type = column.Type;
        //    return type switch
        //    {
        //        Type _ when type == typeof(string) => StringContainsOrdinalIgnoreCase(property, value),
        //        Type _ when type.BaseType == typeof(Enum) => EnumTranslationContains(property, value),
        //        _ => null
        //    };
        //}

        //public Expression<Func<T, bool>> EnumTranslationContains<T>(Expression<Func<T, object>> expression, string value)
        //{
        //    var method = typeof(TableFilterService)
        //            .GetMethod(nameof(EnumTranslationContains), new[] { typeof(Enum), typeof(string) });

        //    var enumContains = Expression.Call(method, expression.Body, Expression.Constant(value));
        //    return Expression.Lambda<Func<T, bool>>(
        //        enumContains,
        //        expression.Parameters);
        //}

        //public Expression<Func<T, bool>> StringContainsOrdinalIgnoreCase<T>(Expression<Func<T, object>> expression, string value)
        //{
        //    return CallMethodType(expression, typeof(string), nameof(string.Contains), new[] { typeof(string), typeof(StringComparison) }, new object[] { value, StringComparison.OrdinalIgnoreCase });
        //}

        //public Expression<Func<T, bool>> CallMethodType<T>(Expression<Func<T, object>> expression, Type type, string method, Type[] parameters, object[] values)
        //{
        //    var methodInfo = type.GetMethod(method, parameters);

        //    return Expression.Lambda<Func<T, bool>>(
        //        Expression.Call(
        //            expression.Body,
        //            methodInfo,
        //            values.OrEmptyIfNull().Select(Expression.Constant)),
        //        expression.Parameters);
        //}

        //public static bool EnumTranslationContains(object value, string text)
        //{
        //    return value is Enum enumValue && enumValue.ToString().Contains(text, StringComparison.OrdinalIgnoreCase);
        //}
    //}
}
