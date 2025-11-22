namespace Despro.Blazor.Base;

//public static class DesproBlazorBasePipeline
//{
//    public static WebApplication UseDesproBlazor(this WebApplication app)
//    {
//        if (app.Environment.IsDevelopment())
//        {
//            //app.UseDeveloperExceptionPage();
//            app.UseExceptionHandler("/Error");
//            app.UseHsts();
//        }
//        else
//        {
//            app.UseExceptionHandler("/Error");
//            app.UseHsts();
//        }

//        app.UseRouting();

//        app.UseHttpsRedirection();

//        app.UseStatusCodePagesWithReExecute("/");

//        app.UseAntiforgery();

//        app.MapStaticAssets();

//        return app;
//    }
//}