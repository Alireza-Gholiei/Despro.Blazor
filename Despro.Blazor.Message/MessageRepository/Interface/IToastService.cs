using Despro.Blazor.Message.MessageGenerals;

namespace Despro.Blazor.Message.MessageRepository.Interface
{
    public interface IToastService
    {
        event Func<Task> OnChanged;
        IEnumerable<ToastModel> Toasts { get; }
        void ErrorAlert(string message = "خطایی در انجام عملیات رخ داده است.");
        void ErrorAlertToast(string message = "خطایی در انجام عملیات رخ داده است.", int delaySeconds = 5);
        void SuccessAlert(string message = "عملیات با موفقیت انجام شد.");
        void SuccessAlertToast(string message = "عملیات با موفقیت انجام شد.", int delaySeconds = 5);
        Task<bool> ConfirmAlert(string message = "آیا از این عملیات اطمینان دارید؟", string confirmText = "تأیید",
            string cancelText = "لغو");
        Task<bool> ConfirmToastAsync(string message = "آیا از این عملیات اطمینان دارید؟", string confirmText = "تأیید",
            string cancelText = "لغو", int delaySeconds = 0);
        void WarningAlert(string message = "خطایی در انجام عملیات رخ داده است.");
        void WarningAlertToast(string message = "خطایی در انجام عملیات رخ داده است.", int delaySeconds = 5);

        Task RemoveToastAsync(ToastModel toast);
    }
}
