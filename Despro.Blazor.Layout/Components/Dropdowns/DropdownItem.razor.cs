using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Layout.Components.Dropdowns
{
    public partial class DropdownItem : BaseComponent, IDisposable
    {
        [CascadingParameter(Name = "Dropdown")] public Dropdown Dropdown { get; set; }
        [CascadingParameter(Name = "DropdownMenu")] public DropdownMenu ParentMenu { get; set; }
        [Parameter] public bool Active { get; set; }
        [Parameter] public bool Disabled { get; set; }
        [Parameter] public bool Highlight { get; set; }
        [Parameter] public RenderFragment SubMenu { get; set; }
        [Parameter] public BaseColor TextColor { get; set; } = BaseColor.Default;
        [Parameter] public BaseColor BackgroundColor { get; set; } = BaseColor.Default;
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }


        private bool hasSubMenu => SubMenu != null;
        private bool subMenuVisible;

        protected override void OnInitialized()
        {
            if (hasSubMenu)
            {
                ParentMenu?.AddSubMenuItem(this);
            }
        }

        private void ItemClicked(MouseEventArgs e)
        {
            if (hasSubMenu)
            {
                ToogleSubMenus(e);
            }
            else if (!hasSubMenu && Dropdown.CloseOnClick)
            {
                Dropdown.Close();
            }

            _ = OnClick.InvokeAsync(e);
        }

        protected override string ClassNames => ClassBuilder
            .Add("dropdown-item")
            .Add(BackgroundColor.GetColorClass("bg"))
            .Add(TextColor.GetColorClass("text"))
            .AddIf("active", Active)
            .AddIf("disabled", Disabled)
            .AddIf("highlight", Highlight)
            .AddIf("dropdown-toggle", hasSubMenu)
            .ToString();


        public void CloseSubMenu()
        {
            subMenuVisible = false;
        }

        private void ToogleSubMenus(MouseEventArgs e)
        {
            bool visible = subMenuVisible;
            ParentMenu?.CloseAllSubMenus();

            subMenuVisible = !visible;

        }

        private string GetWrapperClass()
        {
            return hasSubMenu ? Dropdown.SubMenusDirection == DropdownDirection.Down ? "dropdown" : "dropend" : "";
        }

        public void Dispose()
        {
            ParentMenu?.RemoveSubMenuItem(this);

        }
    }
}
