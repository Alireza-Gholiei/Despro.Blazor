using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Despro.Blazor.Layout.Components.IconsModel;
using Despro.Blazor.Message.MessageGenerals;
using Despro.Blazor.Message.MessageRepository.Interface;
using Despro.Blazor.Modal.ModalGenerals;
using Despro.Blazor.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

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

        #region Tools
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

        public async Task RemoveToastAsync(ToastModel toast)
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
        #endregion

        public void SuccessAlert(string message = "عملیات با موفقیت انجام شد.")
        {
            _ = _modalService.ShowDialogAsync(new DialogOptions
            {
                MainText = "موفق",
                SubText = message,
                IconType = BaseIcons.Success,
                StatusColor = BaseColor.Success,
                CancelText = "",
            });
        }

        public void SuccessAlertToast(string message = "عملیات با موفقیت انجام شد.", int delaySeconds = 5)
        {
            ToastOptions options = new()
            {
                Delay = delaySeconds,
                ShowHeader = true,
                ShowProgress = true,
                
            };

            _ = AddToastAsync(new ToastModel
            {
                Title = "موفق",
                Message = message,
                Options = options,
                IconType = BaseIcons.Success,
                StatusColor = BaseColor.Success,
            });
        }

        public async Task<bool> ConfirmAlert(string message = "آیا از این عملیات اطمینان دارید؟", string confirmText = "تأیید", string cancelText = "لغو")
        {
            return await _modalService.ShowDialogAsync(new DialogOptions
            {
                MainText = "تأیید عملیات",
                OkText = confirmText,
                SubText = message,
                IconType = BaseIcons.Info_circle,
                CancelText = cancelText,
                StatusColor = BaseColor.Primary,
            });
        }

        public Task<bool> ConfirmToastAsync(string message = "آیا از این عملیات اطمینان دارید؟", string confirmText = "تأیید", string cancelText = "لغو", int delaySeconds = 0)
        {
            var tcs = new TaskCompletionSource<bool>();

            ToastOptions options = new()
            {
                Delay = delaySeconds,
                ShowHeader = true,
                ShowProgress = delaySeconds > 0,
                ShowClose = false
            };

            ToastModel toast = null;

            toast = new ToastModel
            {
                Title = "تأیید عملیات",
                Message = message,
                Options = options,
                IconType = BaseIcons.Info_circle,
                Contents = Content
            };

            _ = AddToastAsync(toast);

            if (delaySeconds > 0)
            {
                _ = Task.Delay(delaySeconds * 1000).ContinueWith(async _ =>
                {
                    if (!tcs.Task.IsCompleted)
                    {
                        tcs.TrySetResult(false);
                        await RemoveToastAsync(toast);
                    }
                });
            }

            return tcs.Task;

            void Content(RenderTreeBuilder builder)
            {
                var seq = 0;

                builder.OpenElement(seq++, "div");
                builder.AddAttribute(seq++, "class", "d-flex justify-content-center gap-2");

                builder.OpenElement(seq++, "button");
                builder.AddAttribute(seq++, "class", "btn btn-green px-3 py-2");
                builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create(this, () =>
                {
                    tcs.TrySetResult(true);
                    _ = RemoveToastAsync(toast);
                }));
                builder.AddContent(seq++, confirmText);
                builder.CloseElement();

                builder.OpenElement(seq++, "button");
                builder.AddAttribute(seq++, "class", "btn btn-danger px-3 py-2");
                builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create(this, () =>
                {
                    tcs.TrySetResult(false);
                    _ = RemoveToastAsync(toast);
                }));
                builder.AddContent(seq++, cancelText);
                builder.CloseElement();

                builder.CloseElement();
            }
        }

        public void WarningAlert(string message = "خطایی در انجام عملیات رخ داده است.")
        {
            _ = _modalService.ShowDialogAsync(new DialogOptions
            {
                MainText = "اخطار",
                SubText = message,
                IconType = BaseIcons.Info,
                CancelText = "",
                StatusColor = BaseColor.Orange
            });
        }

        public void WarningAlertToast(string message = "خطایی در انجام عملیات رخ داده است.", int delaySeconds = 5)
        {
            ToastOptions options = new()
            {
                Delay = delaySeconds,
                ShowHeader = true,
                ShowProgress = true
            };

            _ = AddToastAsync(new ToastModel
            {
                Title = "اخطار",
                Message = message,
                Options = options,
                IconType = BaseIcons.Info,
                StatusColor = BaseColor.Orange
            });
        }

        public void ErrorAlert(string message = "خطایی در انجام عملیات رخ داده است.")
        {
            _ = _modalService.ShowDialogAsync(new DialogOptions
            {
                MainText = "خطا",
                SubText = message,
                IconType = BaseIcons.Error,
                CancelText = "",
                StatusColor = BaseColor.Danger
            });
        }

        public void ErrorAlertToast(string message = "خطایی در انجام عملیات رخ داده است.", int delaySeconds = 5)
        {
            ToastOptions options = new()
            {
                Delay = delaySeconds,
                ShowHeader = true,
                ShowProgress = true
            };

            _ = AddToastAsync(new ToastModel
            {
                Title = "خطا",
                Message = message,
                Options = options,
                IconType = BaseIcons.Error,
                StatusColor = BaseColor.Danger
            });
        }

        public void Dispose()
        {
            _listLock?.Dispose();
            OnChanged = null;
        }
    }
}