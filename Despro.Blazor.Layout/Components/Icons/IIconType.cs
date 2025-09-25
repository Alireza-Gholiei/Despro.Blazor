namespace Despro.Blazor.Layout.Components.Icons
{
    public interface IIconType
    {
        public double StrokeWidth { get; }
        public bool Filled { get; }
        public string Elements { get; }
        public string ClassName { get; }
        public IconProvider Provider { get; }

    }

    /// <summary>
    /// Material Design Icons
    /// </summary>
    public class MDIcon : IIconType
    {
        public MDIcon(string elements)
        {
            Elements = elements;
        }
        public double StrokeWidth => 0.1;
        public bool Filled => true;
        public string Elements { get; }
        public string ClassName => "MDIcon";
        public IconProvider Provider => IconProvider.MaterialDesignIcons;
    }

    public class BaseIcon : IIconType
    {
        public BaseIcon(string elements)
        {
            Elements = elements;
        }
        public double StrokeWidth => 2;
        public bool Filled => false;
        public string Elements { get; }
        public string ClassName => "BaseIcon";
        public IconProvider Provider => IconProvider.BaseIcons;
    }

    public enum IconProvider
    {
        Other = 0,
        BaseIcons = 1,
        MaterialDesignIcons = 2
    }
}
