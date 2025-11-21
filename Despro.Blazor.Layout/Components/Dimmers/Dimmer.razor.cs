using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Layout.Components.Dimmers
{
    public partial class Dimmer : BaseComponent
    {
        [Parameter] public bool Active { get; set; }
        [Parameter] public bool ShowSpinner { get; set; } = true;
        [Parameter] public string Text { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected override string ClassNames => ClassBuilder
          .Add("dimmer")
          .AddIf("active", Active)
          .ToString();
    }
}
