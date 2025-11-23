using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Layout.Components.Navbars;

public partial class Navbar : BaseComponent, IDisposable
{
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Parameter] public NavbarBackground Background { get; set; }
    [Parameter] public NavbarDirection Direction { get; set; }
    [Parameter] public NavLinkMatch? NavLinkMatch { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; } = null!;
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    [Parameter] public bool IsNavbarToggler { get; set; } = true;

    private string HtmlTag => "div";
    public bool IsExpanded;

    private readonly List<NavbarMenuItem> navbarItems = new();

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += LocationChanged;
        base.OnInitialized();
    }

    private void LocationChanged(object sender, LocationChangedEventArgs e)
    {
        if (Direction == NavbarDirection.Horizontal)
        {
            CloseAll();
        }
    }

    protected override string ClassNames => ClassBuilder
        .Add("navbar navbar-expand-md")
        .AddIf("navbar-dark", Background == NavbarBackground.Dark)
        .AddIf("navbar-light", Background == NavbarBackground.Light)
        .AddIf("navbar-transparent", Background == NavbarBackground.Transparent)
        .AddIf("navbar-vertical", Direction == NavbarDirection.Vertical)
        .ToString();

    public void ToogleExpand()
    {
        IsExpanded = !IsExpanded;
    }

    public void CloseAll()
    {
        foreach (var item in navbarItems.Where(e => e.IsTopMenuItem))
        {
            item.CloseDropdown();
        }

        StateHasChanged();
    }

    public void AddNavbarMenuItem(NavbarMenuItem item)
    {
        if (!navbarItems.Contains(item))
        {
            navbarItems.Add(item);
        }
    }

    public void RemoveNavbarMenuItem(NavbarMenuItem item)
    {
        if (navbarItems.Contains(item))
        {
            _ = navbarItems.Remove(item);
        }
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= LocationChanged;
    }
}