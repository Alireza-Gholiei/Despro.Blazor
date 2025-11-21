using Despro.Blazor.Base.BaseGenerals;
using Despro.Blazor.Base.Services;
using Despro.Blazor.Base.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Despro.Blazor.Base
{
    public static class DesproBlazorBaseDependencyInjection
    {
        public static IServiceCollection AddDesproBlazorBase(this IServiceCollection services,
            bool persianInitialize = false
            //, ProjectType projectType = ProjectType.BlazorServer
            )
        {
            if (persianInitialize)
            {
                DatePersian.Cultures.InitializePersianCulture();
            }

            _ = services.AddScoped<BaseService>();
            _ = services.AddScoped<IFormValidator, BaseDataAnnotationsValidator>();
            _ = services.AddSingleton<AppService>();

            //switch (projectType)
            //{
            //    case ProjectType.BlazorServer:
            //        services.AddRazorComponents()
            //            .AddInteractiveServerComponents();
            //        break;
            //    case ProjectType.BlazorWebAssembly:
            //        services.AddRazorComponents()
            //            .AddInteractiveWebAssemblyComponents();
            //        break;
            //    case ProjectType.BlazorHybrid:
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException(nameof(projectType), projectType, null);
            //}

            return services;
        }
    }

    public enum ProjectType
    {
        BlazorServer,
        BlazorWebAssembly,
        BlazorHybrid
    }
}
