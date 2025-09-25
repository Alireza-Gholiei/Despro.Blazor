namespace Despro.Blazor.Base.BaseGenerals
{
    public static class EnumHelper
    {
        public static List<TEnum> GetList<TEnum>() where TEnum : struct, Enum
        {
            return !typeof(TEnum).IsEnum ? throw new InvalidOperationException() : Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList();
        }

        public static List<TEnum?> GetNullableList<TEnum>() where TEnum : struct, Enum
        {
            return Enum.GetValues(Nullable.GetUnderlyingType(typeof(TEnum)) ?? typeof(TEnum)).Cast<TEnum?>().ToList();
        }
    }
}
