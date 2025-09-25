using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Layout.Components.Icons;

namespace Despro.Blazor.Modal.ModalGenerals
{
    public class DialogOptions
    {
        public string MainText { get; set; }
        public string SubText { get; set; }
        public IIconType IconType { get; set; }
        public string CancelText { get; set; } = "لغو";
        public string OkText { get; set; } = "تائید";

        public BaseColor StatusColor = BaseColor.Default;
    }
}
