using Despro.Blazor.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Despro.Blazor.Layout
{
    public static class DesproBlazorLayoutDependencyInjection
    {
        public static IServiceCollection AddDesproBlazorLayout(this IServiceCollection services, bool persianInitialize = false)
        {
            _ = services.AddDesproBlazorBase(persianInitialize);

            return services;
        }
    }
}
