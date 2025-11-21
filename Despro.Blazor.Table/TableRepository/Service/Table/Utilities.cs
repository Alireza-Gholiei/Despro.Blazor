using System.Linq.Expressions;
using System.Reflection;

namespace Despro.Blazor.Table.TableRepository.Service.Table
{
    internal static class Utilities
    {
        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source)
        {
            try
            {
                return source ?? [];
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Type GetNonNullableType(this Type type)
        {
            try
            {
                return Nullable.GetUnderlyingType(type) ?? type;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Type GetMemberUnderlyingType(this MemberInfo member)
        {
            try
            {
                if (member == null)
                {
                    return null;
                }

                switch (member.MemberType)
                {
                    case MemberTypes.Field:
                        return ((FieldInfo)member).FieldType;
                    case MemberTypes.Property:
                        return ((PropertyInfo)member).PropertyType;
                    case MemberTypes.Event:
                        return ((EventInfo)member).EventHandlerType;
                    default:
                        throw new ArgumentException("MemberInfo must be if type FieldInfo, PropertyInfo or EventInfo", nameof(member));
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static MemberInfo GetPropertyMemberInfo<T>(this Expression<Func<T, object>> expression)
        {
            try
            {
                if (expression == null)
                {
                    return null;
                }

                if (!(expression.Body is MemberExpression body))
                {
                    UnaryExpression ubody = (UnaryExpression)expression.Body;
                    body = ubody.Operand as MemberExpression;
                }

                return body?.Member;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
