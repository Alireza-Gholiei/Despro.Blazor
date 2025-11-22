using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Services;
using Despro.Blazor.Base.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Despro.Blazor.Base
{
    public static class DesproBlazorBaseDependencyInjection
    {
        public static IServiceCollection AddDesproBlazorBase(this IServiceCollection services,
            bool persianInitialize = false)
        {
            if (persianInitialize)
            {
                DatePersian.Cultures.InitializePersianCulture();
            }

            _ = services.AddScoped<BaseService>();
            _ = services.AddScoped<IFormValidator, BaseDataAnnotationsValidator>();
            _ = services.AddSingleton<AppService>();

            return services;
        }
    }
}
