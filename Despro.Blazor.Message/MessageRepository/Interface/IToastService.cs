using Despro.Blazor.Message.MessageGenerals;

namespace Despro.Blazor.Message.MessageRepository.Interface
{
    public interface IToastService
    {
        event Func<Task> OnChanged;
        IEnumerable<ToastModel> Toasts { get; }
        void ErrorAlert(string message = "خطایی در انجام عملیات رخ داده است.");
        void ErrorAlertToast(string message = "خطایی در انجام عملیات رخ داده است.");
        void SuccessAlert(string message = "عملیات با موفقیت انجام شد.");
        void SuccessAlertToast(string message = "عملیات با موفقیت انجام شد.");
        void WarningAlert(string message = "خطایی در انجام عملیات رخ داده است.");
        void WarningAlertToast(string message = "خطایی در انجام عملیات رخ داده است.");
    }
}
