using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Layout.Components.Cards
{
    public enum CardSize
    {
        Default,
        Small,
        Medium,
        Large
    }
    public enum CardStatusPosition
    {
        Left,
        Top,
        Bottom
    }

    public partial class Card : BaseComponent
    {
        [Parameter] public CardSize Size { get; set; } = CardSize.Default;
        [Parameter] public bool Stacked { get; set; }
        [Parameter] public BaseColor StatusTop { get; set; } = BaseColor.Default;
        [Parameter] public BaseColor StatusStart { get; set; } = BaseColor.Default;
        [Parameter] public string LinkTo { get; set; }
        [Parameter] public string Style { get; set; }
        [Parameter] public BaseColor TextColor { get; set; } = BaseColor.Default;
        [Parameter] public BaseColor BackgroundColor { get; set; } = BaseColor.Default;
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected string HtmlTag => string.IsNullOrWhiteSpace(LinkTo)
            ? "div"
            : "a";

        protected string? Href => !string.IsNullOrWhiteSpace(LinkTo)
            ? LinkTo
            : null;

        protected override string ClassNames => ClassBuilder
            .Add("card")
            .AddIf("card-stacked", Stacked)
            .Add(BackgroundColor.GetColorClass("bg"))
            .Add(TextColor.GetColorClass("text"))
            .AddCompare("card-sm", Size, CardSize.Small)
            .AddCompare("card-md", Size, CardSize.Medium)
            .AddCompare("card-lg", Size, CardSize.Large)
            .ToString();

        protected string StatusClassNames(string position, BaseColor color)
        {
            return ClassBuilder
                .Add($"card-status-{position}")
                .Add(color.GetColorClass("bg"))
                .ToString();
        }
    }
}