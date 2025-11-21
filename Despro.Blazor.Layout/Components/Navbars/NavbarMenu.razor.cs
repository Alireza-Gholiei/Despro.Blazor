using Despro.Blazor.Base.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Despro.Blazor.Layout.Components.Navbars
{
    public partial class NavbarMenu : BaseComponent
    {
        [CascadingParameter(Name = "Navbar")] private Navbar Navbar { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        private bool isExpanded => Navbar.IsExpanded;
        protected string HtmlTag => "div";
        protected override string ClassNames => ClassBuilder
              .Add("navbar-collapse")
              .AddIf("collapse", !isExpanded)
              .ToString();

        public void ToogleExpanded()
        {

            Navbar.ToogleExpand();
        }

        private string menuCollapse => isExpanded ? "" : "collapse";
    }
}
