using System.ComponentModel;

namespace Despro.Blazor.Base.DesproBlazorExceptions
{
    public static class Extensions
    {

        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example><![CDATA[string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            Type type = enumVal.GetType();
            System.Reflection.MemberInfo[] memInfo = type.GetMember(enumVal.ToString());
            object[] attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? (T)attributes[0] : null;
        }

        public static List<KeyValuePair<TEnum, string>> GetList<TEnum>()
       where TEnum : struct
        {
            return !typeof(TEnum).IsEnum
                ? throw new InvalidOperationException()
                : ((TEnum[])Enum.GetValues(typeof(TEnum)))
               .ToDictionary(k => k, v => ((Enum)(object)v).GetAttributeOfType<DescriptionAttribute>().Description)
               .ToList();
        }

    }
}
