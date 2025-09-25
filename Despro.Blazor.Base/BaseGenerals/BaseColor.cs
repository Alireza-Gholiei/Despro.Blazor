namespace Despro.Blazor.Base.BaseGenerals
{
    public enum BaseColor
    {
        Default,
        Blue,
        Azure,
        Indigo,
        Purple,
        Pink,
        Red,
        Orange,
        Yellow,
        Lime,
        Green,
        Teal,
        Cyan,
        White,
        Primary,
        Secondary,
        Success,
        Info,
        Warning,
        Danger,
        Light,
        Dark
    }

    public enum ColorType
    {
        Default,
        Outline,
        Ghost
    }

    public static class ColorsExtensions
    {
        public static string GetColorClass(this BaseColor color, string type,
            ColorType colorType = ColorType.Default, string suffix = "")
        {
            string colorClass = $"{type}";

            colorClass += colorType switch
            {
                ColorType.Default => "",
                _ => $"-{Enum.GetName(typeof(ColorType), colorType)?.ToLower()}"
            };

            colorClass = color switch
            {
                BaseColor.Default => "",
                _ => $"{colorClass}-{Enum.GetName(typeof(BaseColor), color)?.ToLower()}"
            };

            if (color != BaseColor.Light && colorClass.ToLower().EndsWith("light"))
            {
                colorClass = colorClass.Replace("light", "-lt", StringComparison.InvariantCultureIgnoreCase);
            }

            if (!string.IsNullOrWhiteSpace(suffix) && !string.IsNullOrWhiteSpace(colorClass))
                colorClass += $"-{suffix}";

            return colorClass;
        }
    }
}