using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Layout.Components
{
    public partial class Row : BaseComponent
    {
        [Parameter] public bool HasCards { get; set; }
        [Parameter] public BaseColor TextColor { get; set; } = BaseColor.Default;
        [Parameter] public BaseColor BackgroundColor { get; set; } = BaseColor.Default;
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected override string ClassNames => ClassBuilder
            .Add("row")
            .Add(BackgroundColor.GetColorClass("bg"))
            .Add(TextColor.GetColorClass("text"))
            .AddIf("row-cards", HasCards)
            .ToString();
    }
}