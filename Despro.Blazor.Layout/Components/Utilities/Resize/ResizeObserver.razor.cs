using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Despro.Blazor.Layout.Components.Utilities.Resize
{
    public partial class ResizeObserver : BaseComponent
    {
        [Inject] private IJSRuntime JsRuntime { get; set; }

        [Parameter] public string Tag { get; set; } = "div";
        [Parameter] public EventCallback<ResizeObserverEntry> OnResized { get; set; }
        [Parameter] public EventCallback<ResizeObserverEntry> OnWidthResized { get; set; }
        [Parameter] public EventCallback<ResizeObserverEntry> OnHeightResized { get; set; }
        [Parameter] public BaseColor TextColor { get; set; } = BaseColor.Default;
        [Parameter] public BaseColor BackgroundColor { get; set; } = BaseColor.Default;
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }


        private ElementReference elementRef;
        private ResizeObserverEntry currentEntry;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsRuntime.InvokeVoidAsync("DesproBlazor.addResizeObserver", elementRef, DotNetObjectReference.Create(this));
            }
        }

        protected override string ClassNames => ClassBuilder
            .Add(BackgroundColor.GetColorClass("bg"))
            .Add(TextColor.GetColorClass("text"))
            .ToString();

        [JSInvokable]
        public async Task ElementResized(ResizeObserverEntry resizeObserverEntry)
        {
            await OnResized.InvokeAsync(resizeObserverEntry);
            if (currentEntry?.ContentRect?.Width != resizeObserverEntry?.ContentRect?.Width)
            {
                await OnWidthResized.InvokeAsync(resizeObserverEntry);
            }

            if (currentEntry?.ContentRect?.Height != resizeObserverEntry?.ContentRect?.Height)
            {
                await OnHeightResized.InvokeAsync(resizeObserverEntry);
            }

            currentEntry = resizeObserverEntry;
        }
    }
}