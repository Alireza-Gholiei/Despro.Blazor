using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Layout.Components.Dropdowns
{
    public partial class DropdownMenu : BaseComponent, IDisposable
    {
        [Parameter] public bool Arrow { get; set; } = false;
        [Parameter] public bool Card { get; set; } = false;
        [Parameter] public EventCallback Disposed { get; set; }
        [Parameter] public BaseColor TextColor { get; set; } = BaseColor.Default;
        [Parameter] public BaseColor BackgroundColor { get; set; } = BaseColor.Default;
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }


        private readonly List<DropdownItem> subMenuItems = new();

        protected override string ClassNames => ClassBuilder
           .Add("dropdown-menu")
           .Add(BackgroundColor.GetColorClass("bg"))
           .Add(TextColor.GetColorClass("text"))
           .AddIf("show", true)
           .AddIf($"dropdown-menu-arrow", Arrow)
           .AddIf($"dropdown-menu-card", Card)
           .ToString();

        public void CloseAllSubMenus()
        {
            foreach (DropdownItem item in subMenuItems)
            {
                item.CloseSubMenu();
            }
            StateHasChanged();
        }

        public void AddSubMenuItem(DropdownItem item)
        {
            if (item != null && !subMenuItems.Contains(item))
            {
                subMenuItems.Add(item);
            }
        }

        public void RemoveSubMenuItem(DropdownItem item)
        {
            if (item != null && subMenuItems.Contains(item))
            {
                _ = subMenuItems.Remove(item);
            }
        }

        public void Dispose()
        {
            if (Disposed.HasDelegate)
            {
                _ = Disposed.InvokeAsync();
            }
        }
    }
}
