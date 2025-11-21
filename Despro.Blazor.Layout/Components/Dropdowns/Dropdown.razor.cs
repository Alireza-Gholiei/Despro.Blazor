using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Layout.Components.Dropdowns
{
    public partial class Dropdown : BaseComponent
    {
        [Parameter] public RenderFragment DropdownTemplate { get; set; }
        [Parameter] public bool CloseOnClick { get; set; } = true;
        [Parameter] public DropdownDirection Direction { get; set; }
        [Parameter] public DropdownDirection SubMenusDirection { get; set; } = DropdownDirection.End;
        [Parameter] public EventCallback<bool> OnExpanded { get; set; }
        [Parameter] public BaseColor TextColor { get; set; } = BaseColor.Default;
        [Parameter] public BaseColor BackgroundColor { get; set; } = BaseColor.Default;
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }


        public bool IsExpanded => isExpanded;

        protected bool isExpanded;

        private double top;
        private double left;
        private bool isContextMenu;

        protected override string ClassNames => ClassBuilder
            .AddIf("dropdown", Direction == DropdownDirection.Down)
            .AddIf("dropend", Direction == DropdownDirection.End)
            .Add("cursor-pointer")
            .Add(BackgroundColor.GetColorClass("bg"))
            .Add("cursor-pointer")
            .Add(BackgroundColor.GetColorClass("bg"))
            .Add(TextColor.GetColorClass("text"))
            .ToString();

        private void SetExpanded(bool expanded)
        {
            isExpanded = expanded;
            _ = OnExpanded.InvokeAsync(isExpanded);
        }

        protected void OnClickOutside()
        {
            if (isExpanded)
            {
                SetExpanded(false);
            }
        }

        private string GetSyle()
        {
            return isContextMenu ? $"position:fixed;top:{top}px;left:{left}px" : "";
        }

        protected async Task OnDropdownClick(MouseEventArgs e)
        {
            await OnClick.InvokeAsync(e);
            Toogle();
        }

        public void Toogle()
        {
            SetExpanded(!isExpanded);
        }

        public void Open()
        {
            SetExpanded(true);
        }

        public void OpenAsContextMenu(MouseEventArgs e)
        {
            isContextMenu = true;
            top = e.ClientY;
            left = e.ClientX;
            SetExpanded(true);
            _ = InvokeAsync(StateHasChanged);

        }

        public void Close()
        {
            SetExpanded(false);
            _ = InvokeAsync(StateHasChanged);
        }
    }
}