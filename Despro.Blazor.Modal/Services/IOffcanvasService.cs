using Despro.Blazor.Base.Components;
using Despro.Blazor.Modal.ModalGenerals;

namespace Despro.Blazor.Modal.Services
{
    public interface IOffcanvasService
    {
        IEnumerable<OffcanvasModel> Models { get; }

        event Action OnChanged;

        void Close();
        Task<OffcanvasResult> ShowAsync<TComponent>(string title, RenderComponent<TComponent> component, OffcanvasOptions options = null) where TComponent : BaseComponent;
    }
}