using Despro.Blazor.Layout.Components.Icons;

namespace Despro.Blazor.Layout.Components.IconsModel
{
    public class GeneratedIcon
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public List<string> Tags { get; set; }
        public IIconType IconType { get; set; }
        public string DotNetProperty => $"public static IIconType {GetSafeName()} => new {IconType.ClassName}(@\"{IconType?.Elements}\");";

        public string GetSafeName()
        {
            string safeName = Name;
            safeName = safeName.Replace("-", "_");
            safeName = char.IsDigit(safeName.ToCharArray().First()) ? "_" + safeName : FirstCharacterToUpperCase(safeName);

            return safeName;
        }


        private static string FirstCharacterToUpperCase(string text)
        {
            return string.IsNullOrWhiteSpace(text)
                ? text
                : text.Length == 1 ? char.ToUpper(text[0]).ToString() : char.ToUpper(text[0]) + text.Substring(1);
        }

    }
}
