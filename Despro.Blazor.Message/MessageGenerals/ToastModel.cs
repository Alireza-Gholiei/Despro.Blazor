using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Layout.Components.Icons;
using Microsoft.AspNetCore.Components;

namespace Despro.Blazor.Message.MessageGenerals
{
    public class ToastModel
    {

        public ToastModel() { }

        public ToastModel(string title, string subTitle, string message, ToastOptions options = null)
        {
            Title = title;
            SubTitle = subTitle;
            Message = message;
            Options = options ?? new ToastOptions();
        }

        public ToastModel(string title, string subTitle, RenderFragment contents, ToastOptions options = null)
        {
            Title = title;
            SubTitle = subTitle;
            Contents = contents;
            Options = options ?? new ToastOptions();
        }

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Message { get; set; }
        public IIconType IconType { get; set; }
        public BaseColor StatusColor { get; set; } = BaseColor.Default;
        public ToastOptions Options { get; set; } = new ToastOptions();
        public RenderFragment Contents { get; internal set; }
    }
}
