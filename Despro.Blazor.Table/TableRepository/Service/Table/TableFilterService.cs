using Despro.Blazor.Table.TableRepository.Interface.Table;
using System.Linq.Expressions;
using System.Reflection;

namespace Despro.Blazor.Table.TableRepository.Service.Table
{
    public class TableFilterService
    {
        public Expression<Func<T, bool>> GetFilter<T>(IColumn<T> column, string value)
        {
            Expression<Func<T, object>> property = column.Property;

            if (column.SearchExpression != null)
            {
                InvocationExpression wrappedExpression = Expression.Invoke(column.SearchExpression, property.Parameters[0], Expression.Constant(value));
                return Expression.Lambda<Func<T, bool>>(wrappedExpression, property.Parameters);
            }

            Type type = column.Type;
            switch (type)
            {
                case Type _ when type == typeof(string):
                    return StringContainsOrdinalIgnoreCase(property, value);
                case Type _ when type.BaseType == typeof(Enum):
                    return EnumTranslationContains(property, value);
                default:
                    return null;
            }
        }

        public Expression<Func<T, bool>> EnumTranslationContains<T>(Expression<Func<T, object>> expression, string value)
        {
            MethodInfo method = typeof(TableFilterService)
                    .GetMethod(nameof(EnumTranslationContains), new[] { typeof(Enum), typeof(string) });

            MethodCallExpression enumContains = Expression.Call(method, expression.Body, Expression.Constant(value));
            return Expression.Lambda<Func<T, bool>>(
                enumContains,
                expression.Parameters);
        }

        public Expression<Func<T, bool>> StringContainsOrdinalIgnoreCase<T>(Expression<Func<T, object>> expression, string value)
        {
            return CallMethodType(expression, typeof(string), nameof(string.Contains), new[] { typeof(string), typeof(StringComparison) }, new object[] { value, StringComparison.OrdinalIgnoreCase });
        }

        public Expression<Func<T, bool>> CallMethodType<T>(Expression<Func<T, object>> expression, Type type, string method, Type[] parameters, object[] values)
        {
            MethodInfo methodInfo = type.GetMethod(method, parameters);

            return Expression.Lambda<Func<T, bool>>(
                Expression.Call(
                    expression.Body,
                    methodInfo,
                    values.OrEmptyIfNull().Select(Expression.Constant)),
                expression.Parameters);
        }

        public static bool EnumTranslationContains(object value, string text)
        {
            return !(value is Enum enumValue) ? false : enumValue.ToString().Contains(text, StringComparison.OrdinalIgnoreCase);
        }
    }
}
