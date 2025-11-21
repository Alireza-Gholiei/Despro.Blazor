using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Layout.Components.Navbars
{
    public partial class NavbarMenuItem : BaseComponent, IDisposable
    {
        [CascadingParameter(Name = "Navbar")] private Navbar Navbar { get; set; }
        [CascadingParameter(Name = "Parent")] private NavbarMenuItem ParentMenuItem { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }

        [Parameter] public string Href { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public RenderFragment MenuItemIcon { get; set; }
        [Parameter] public RenderFragment SubMenu { get; set; }
        [Parameter] public bool Expanded { get; set; }
        [Parameter] public bool Expandable { get; set; } = true;
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        public bool IsTopMenuItem => ParentMenuItem == null;

        protected string HtmlTag => "li";
        protected bool IsExpanded;

        protected bool IsDropdown => SubMenu != null && Expandable;

        protected bool IsSubMenu => ParentMenuItem != null;

        protected override void OnInitialized()
        {
            IsExpanded = Expanded;
            Navbar?.AddNavbarMenuItem(this);

            NavigationManager.LocationChanged += LocationChanged;
        }

        private void LocationChanged(object sender, LocationChangedEventArgs e)
        {
            StateHasChanged();
        }

        private bool IsActive()
        {
            if (Href == null) { return false; }

            if (Navbar.NavLinkMatch == null) { return false; }

            NavLinkMatch navLinkMatch = (NavLinkMatch)Navbar.NavLinkMatch;

            string relativePath = NavigationManager.ToBaseRelativePath(NavigationManager.Uri).ToLower();
            return navLinkMatch == NavLinkMatch.All ? relativePath == Href.ToLower() : relativePath.StartsWith(Href.ToLower());
        }


        private bool NavbarIsHorizontalAndDark => Navbar?.Background == NavbarBackground.Dark && Navbar?.Direction == NavbarDirection.Horizontal;

        private bool IsDropEnd => Navbar.Direction == NavbarDirection.Horizontal && ParentMenuItem?.IsDropdown == true;

        protected override string ClassNames => ClassBuilder
            .Add("nav-item")
            .Add("cursor-pointer")
            .AddIf("dropdown", IsDropdown && !IsDropEnd)
            .AddIf("dropend", IsDropdown && IsDropEnd)
            .AddIf("active", IsActive())
            .ToString();

        public void CloseDropdown()
        {
            IsExpanded = false;
        }

        public void ToogleDropdown()
        {
            bool expand = !IsExpanded;

            if (expand && IsTopMenuItem)
            {
                Navbar.CloseAll();
            }

            IsExpanded = expand;
        }

        public void Dispose()
        {
            Navbar?.RemoveNavbarMenuItem(this);

            if (NavigationManager != null)
            {
                NavigationManager.LocationChanged -= LocationChanged;
            }
        }
    }
}

