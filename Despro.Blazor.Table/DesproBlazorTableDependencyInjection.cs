using Despro.Blazor.Layout;
using Microsoft.Extensions.DependencyInjection;

namespace Despro.Blazor.Table
{
    public static class DesproBlazorTableDependencyInjection
    {
        public static IServiceCollection AddDesproBlazorTable(this IServiceCollection services, bool persianInitialize = false)
        {
            _ = services.AddDesproBlazorLayout(persianInitialize);

            //_ = services.AddScoped<TableFilterService>();

            //_ = services.Configure<BaseOptions>(options =>
            //{
            //    options.AssemblyScanFilter = () => [typeof(Flags.Flags).Assembly];
            //});

            return services;
        }
    }
}
