using Despro.Blazor.Base.Components;
using Despro.Blazor.Modal.ModalGenerals;

namespace Despro.Blazor.Modal.Services
{
    public class OffcanvasService : IOffcanvasService
    {

        public event Action OnChanged;
        private readonly Stack<OffcanvasModel> models = new();
        public IEnumerable<OffcanvasModel> Models => models;

        public Task<OffcanvasResult> ShowAsync<TComponent>(string title, RenderComponent<TComponent> component, OffcanvasOptions options = null) where TComponent : BaseComponent
        {
            OffcanvasModel offcanvasModel = new()
            {
                Title = title,
                Contents = component.Contents,
                Options = options ?? new OffcanvasOptions()
            };
            models.Push(offcanvasModel);
            OnChanged?.Invoke();
            return offcanvasModel.Task;
        }

        public void Close()
        {

            if (models.Any())
            {
                _ = models.Pop();
            }

            OnChanged?.Invoke();
        }
    }
}

