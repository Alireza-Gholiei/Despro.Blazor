using Despro.Blazor.Base.BaseGenerals;

namespace Despro.Blazor.Modal.ModalGenerals
{
    public class ModalOptions
    {
        public bool ShowHeader { get; set; } = true;
        public bool ShowCloseButton { get; set; } = true;
        public bool Scrollable { get; set; } = true;
        public bool CloseOnClickOutside { get; set; } = true;
        public bool BlurBackground { get; set; } = true;
        public bool Backdrop { get; set; } = true;
        public bool CloseOnEsc { get; set; } = true;
        public bool Draggable { get; set; } = false;
        public string ModalCssClass { get; set; }
        public string ModalBodyCssClass { get; set; }

        public ModalVerticalPosition VerticalPosition { get; set; } = ModalVerticalPosition.Centered;
        public ModalSize Size { get; set; } = ModalSize.XLarge;
        public ModalFullscreen Fullscreen { get; set; } = ModalFullscreen.Never;
        public BaseColor? StatusColor { get; set; } = BaseColor.Green;
    }

    public enum ModalVerticalPosition
    {
        Default = 0,
        Centered = 1
    }
}
