using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Despro.Blazor.Base;

public static class DesproBlazorBasePipeline
{
    public static IApplicationBuilder UseDesproBlazor(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}