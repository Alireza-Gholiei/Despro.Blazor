using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Layout.Components.Cards
{
    public enum RibbonPosition
    {
        Top,
        Right
    }

    public partial class CardRibbon : BaseComponent
    {
        [Parameter] public RibbonPosition Position { get; set; } = RibbonPosition.Right;
        [Parameter] public BaseColor TextColor { get; set; } = BaseColor.Default;
        [Parameter] public BaseColor BackgroundColor { get; set; } = BaseColor.Default;
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected override string ClassNames => ClassBuilder
            .Add("ribbon")
            .Add(BackgroundColor.GetColorClass("bg"))
            .Add(TextColor.GetColorClass("text"))
            .AddCompare("ribbon-top", Position, RibbonPosition.Top)
            .AddCompare("ribbon-right", Position, RibbonPosition.Right)
            .ToString();
    }
}