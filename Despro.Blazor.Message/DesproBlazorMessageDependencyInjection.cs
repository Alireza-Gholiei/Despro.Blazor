using Despro.Blazor.Message.MessageRepository.Interface;
using Despro.Blazor.Message.MessageRepository.Services;
using Despro.Blazor.Modal;
using Microsoft.Extensions.DependencyInjection;

namespace Despro.Blazor.Message
{
    public static class DesproBlazorMessageDependencyInjection
    {
        public static IServiceCollection AddDesproBlazorMessage(this IServiceCollection services, bool persianInitialize = false)
        {
            _ = services.AddDesproBlazorModal(persianInitialize);
            _ = services.AddScoped<IToastService, ToastService>();
            return services;
        }
    }
}
