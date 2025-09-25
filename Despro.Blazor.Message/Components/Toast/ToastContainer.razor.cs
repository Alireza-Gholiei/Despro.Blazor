using Despro.Blazor.Base.Components;
using Despro.Blazor.Message.MessageRepository.Interface;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Message.Components.Toast
{
    public partial class ToastContainer : BaseComponent
    {
        [Inject] public IToastService ToastService { get; set; }

        protected override void OnInitialized()
        {
            ToastService.OnChanged += OnToastChanged;
        }

        public async Task OnToastChanged()
        {
            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            ToastService.OnChanged -= OnToastChanged;
        }
    }
}