using Despro.Blazor.Base.Components;
using Despro.Blazor.Modal.Components.Modal;
using Despro.Blazor.Modal.ModalGenerals;

namespace Despro.Blazor.Modal.Services
{
    public interface IModalService
    {
        event Action OnChanged;
        IEnumerable<ModalModel> Modals { get; }

        Task<ModalResult> ShowAsync<TComponent>(string title, RenderComponent<TComponent> component, ModalOptions modalOptions = null) where TComponent : BaseComponent;
        Task<ModalResult> ShowAsync<TComponent>(string title, RenderComponent<TComponent> component) where TComponent : BaseComponent;
        void Close(ModalResult modalResult);
        void Close();
        Task<bool> ShowDialogAsync(DialogOptions options);
        void UpdateTitle(string title);
        void Refresh();
        ModalViewSettings RegisterModalView(ModalView modalView);
        void UnRegisterModalView(ModalView modalView);
    }
}
