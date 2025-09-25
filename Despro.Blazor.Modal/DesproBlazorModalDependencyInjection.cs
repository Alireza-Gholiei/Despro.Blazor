using Despro.Blazor.Layout;
using Despro.Blazor.Modal.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Despro.Blazor.Modal
{
    public static class DesproBlazorModalDependencyInjection
    {
        public static IServiceCollection AddDesproBlazorModal(this IServiceCollection services, bool persianInitialize = false)
        {
            _ = services.AddDesproBlazorLayout(persianInitialize);
            _ = services.AddScoped<IModalService, ModalService>();
            _ = services.AddScoped<IOffcanvasService, OffcanvasService>();
            return services;
        }
    }
}
