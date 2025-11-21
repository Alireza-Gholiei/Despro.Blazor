using Despro.Blazor.Base.Components;
using Despro.Blazor.Base.Services;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Form.Components.Checkboxes
{
    public partial class CheckboxTriState : BaseComponent
    {
        private bool isInitialized;
        [Inject] public BaseService BaseService { get; set; }
        [Parameter] public string Label { get; set; }
        [Parameter] public string Description { get; set; }

        [Parameter] public bool? Value { get; set; }

        [Parameter] public EventCallback<bool?> ValueChanged { get; set; }
        [Parameter] public EventCallback Changed { get; set; }
        [Parameter] public bool Disabled { get; set; }

        protected ElementReference Element { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender || isInitialized)
            {
                isInitialized = true;
                await BaseService.SetElementProperty(Element, "indeterminate", !Value.HasValue);
            }
        }

        protected async Task ToggleState()
        {
            Value = Value == null ? true : !Value;

            await ValueChanged.InvokeAsync(Value);
            await Changed.InvokeAsync();
        }
    }
}