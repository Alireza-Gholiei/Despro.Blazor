using Despro.Blazor.Base.Components;
using Despro.Blazor.Modal.ModalGenerals;
using Despro.Blazor.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Modal.Components.Modal
{
    public partial class ModalContainer : BaseComponent, IDisposable
    {
        [Inject] private IModalService ModalService { get; set; }

        protected override void OnInitialized()
        {
            ModalService.OnChanged += StateHasChanged;
        }

        public void Dispose()
        {
            ModalService.OnChanged -= StateHasChanged;
        }

        public void ModalClosed(ModalModel modalModel)
        {
            ModalService.Close();
        }
    }
}
