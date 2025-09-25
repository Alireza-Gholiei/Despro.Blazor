using Despro.Blazor.Base.BaseGenerals;

namespace Despro.Blazor.Base.Services
{
    public class AppSettings
    {
        public bool DarkMode { get; set; } = true;
        public NavbarDirection NavbarDirection { get; set; } = NavbarDirection.Vertical;
        public NavbarBackground NavbarBackground { get; set; } = NavbarBackground.Dark;
    }
}
