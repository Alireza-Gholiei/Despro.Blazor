using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Despro.Blazor.Layout.Components.IconsModel;
using Despro.Blazor.Message.MessageGenerals;
using Despro.Blazor.Message.MessageRepository.Interface;
using Despro.Blazor.Modal.ModalGenerals;
using Despro.Blazor.Modal.Services;

namespace Despro.Blazor.Message.MessageRepository.Services
{
    public class ToastService : IToastService, IDisposable
    {
        private readonly IModalService _modalService;

        public ToastService(IModalService modalService)
        {
            _modalService = modalService;
        }

        public IEnumerable<ToastModel> Toasts => _toasts;
        public event Func<Task> OnChanged;

        private readonly List<ToastModel> _toasts = new();
        private readonly ReaderWriterLockSlim _listLock = new();

        private async Task AddToastAsync(ToastModel toast)
        {
            AddToast(toast);
            await Changed();
        }

        public async Task AddToastAsync<TComponent>(string title, string subTitle, RenderComponent<TComponent> component, ToastOptions options = null) where TComponent : BaseComponent
        {
            ToastModel toast = new(title, subTitle, component?.Contents, options);
            await AddToastAsync(toast);
        }

        private void AddToast(ToastModel toast)
        {
            try
            {
                _listLock.EnterWriteLock();
                _toasts.Add(toast);
            }
            finally
            {
                _listLock.ExitWriteLock();
            }
        }

        public async Task RemoveAllAsync()
        {
            _toasts.Clear();
            await Changed();
        }

        internal async Task RemoveToastAsync(ToastModel toast)
        {
            try
            {
                _listLock.EnterWriteLock();
                if (_toasts.Contains(toast))
                {
                    _ = _toasts.Remove(toast);
                }
            }
            finally
            {
                _listLock.ExitWriteLock();
            }

            await Changed();
        }

        private async Task Changed()
        {
            if (OnChanged != null)
            {
                await OnChanged.Invoke();
            }
        }

        public void SuccessAlert(string message = "عملیات با موفقیت انجام شد.")
        {
            _ = _modalService.ShowDialogAsync(new DialogOptions
            {
                MainText = "موفق",
                SubText = message,
                IconType = BaseIcons.Message,
                CancelText = "",
                StatusColor = BaseColor.Success
            });
        }

        public void SuccessAlertToast(string message = "عملیات با موفقیت انجام شد.")
        {
            ToastOptions options = new()
            {
                Delay = 5,
                ShowHeader = true,
                ShowProgress = true
            };

            _ = AddToastAsync(new ToastModel
            {
                Title = "موفق",
                Message = message,
                Options = options,
                Icon = ToastIcon.Success
            });
        }

        public void WarningAlert(string message = "خطایی در انجام عملیات رخ داده است.")
        {
            _ = _modalService.ShowDialogAsync(new DialogOptions
            {
                MainText = "اخطار",
                SubText = message,
                IconType = BaseIcons.Message,
                CancelText = "",
                StatusColor = BaseColor.Orange
            });
        }

        public void WarningAlertToast(string message = "خطایی در انجام عملیات رخ داده است.")
        {
            ToastOptions options = new()
            {
                Delay = 5,
                ShowHeader = true,
                ShowProgress = true
            };

            _ = AddToastAsync(new ToastModel
            {
                Title = "اخطار",
                Message = message,
                Options = options,
                Icon = ToastIcon.Warning
            });
        }

        public void ErrorAlert(string message = "خطایی در انجام عملیات رخ داده است.")
        {
            _ = _modalService.ShowDialogAsync(new DialogOptions
            {
                MainText = "خطا",
                SubText = message,
                IconType = BaseIcons.Message,
                CancelText = "",
                StatusColor = BaseColor.Danger
            });
        }

        public void ErrorAlertToast(string message = "خطایی در انجام عملیات رخ داده است.")
        {
            ToastOptions options = new()
            {
                Delay = 5,
                ShowHeader = true,
                ShowProgress = true
            };

            _ = AddToastAsync(new ToastModel
            {
                Title = "خطا",
                Message = message,
                Options = options,
                Icon = ToastIcon.Error
            });
        }

        public void Dispose()
        {

        }
    }
}