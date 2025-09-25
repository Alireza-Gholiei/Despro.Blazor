using Despro.Blazor.Base.Components;
using Despro.Blazor.Modal.Components.Modal;
using Despro.Blazor.Modal.Components.Modal.Standard;
using Despro.Blazor.Modal.ModalGenerals;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Despro.Blazor.Modal.Services
{
    internal class ModalService : IModalService, IDisposable
    {
        private readonly NavigationManager _navigationManager;

        public ModalService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _navigationManager.LocationChanged += LocationChanged;
        }

        private int _zIndex = 1200;
        private const int ZIndexIncrement = 10;
        private int _topOffset;
        private const int TopOffsetIncrement = 20;

        public event Action OnChanged;
        private readonly Stack<ModalModel> _modals = new();
        internal ModalModel ModalModel;

        public IEnumerable<ModalModel> Modals => _modals;

        public Task<ModalResult> ShowAsync<TComponent>(string title, RenderComponent<TComponent> component, ModalOptions modalOptions = null) where TComponent : BaseComponent
        {
            ModalModel = new ModalModel(component.Contents, title, modalOptions);
            _modals.Push(ModalModel);
            OnChanged?.Invoke();
            return ModalModel.Task;
        }

        public Task<ModalResult> ShowAsync<TComponent>(string title, RenderComponent<TComponent> component) where TComponent : BaseComponent
        {
            ModalOptions modalOptions = new();

            ModalModel = new ModalModel(component.Contents, title, modalOptions);
            _modals.Push(ModalModel);
            OnChanged?.Invoke();
            return ModalModel.Task;
        }

        public async Task<bool> ShowDialogAsync(DialogOptions options)
        {
            RenderComponent<DialogModal> component = new RenderComponent<DialogModal>().
                Set(e => e.Options, options);
            ModalResult result = await ShowAsync("", component, new ModalOptions { ModalBodyCssClass = "p-0", Size = ModalSize.Small, ShowHeader = false, StatusColor = options.StatusColor });
            return !result.Cancelled;
        }

        private void LocationChanged(object sender, LocationChangedEventArgs e)
        {
            CloseAll();
        }

        private void CloseAll()
        {
            foreach (ModalModel x in _modals.ToList())
            {
                Close();
            }
        }

        public void Close(ModalResult modalResult)
        {
            if (_modals.Any())
            {
                ModalModel modalToClose = _modals.Pop();
                modalToClose.TaskSource.SetResult(modalResult);
            }

            OnChanged?.Invoke();
        }

        public void Close()
        {
            Close(ModalResult.Cancel());
        }

        public void UpdateTitle(string title)
        {
            ModalModel modal = Modals.LastOrDefault();
            if (modal != null)
            {
                modal.Title = title;
                OnChanged?.Invoke();
            }
        }

        public void Refresh()
        {
            ModalModel modal = Modals.LastOrDefault();
            if (modal != null)
            {
                OnChanged?.Invoke();
            }
        }

        public ModalViewSettings RegisterModalView(ModalView modalView)
        {
            ModalViewSettings settings = new() { TopOffset = _topOffset, ZIndex = _zIndex };
            _zIndex += ZIndexIncrement;
            _topOffset += TopOffsetIncrement;

            return settings;
        }

        public void UnRegisterModalView(ModalView modalView)
        {
            _zIndex -= ZIndexIncrement;
            _topOffset -= TopOffsetIncrement;
        }

        public void Dispose()
        {
            _navigationManager.LocationChanged -= LocationChanged;
        }
    }
}
