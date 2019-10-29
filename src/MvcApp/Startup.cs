using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace MvcApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddTransient<CustomLocalizer>();
            services.AddRouting();
            services.AddSingleton<TranslationTransformer>();
            services.AddSingleton<TranslationDatabase>();
            services.AddMvc();//.SetCompatibilityVersion(CompatibilityVersion.Latest);

        }

        public void Configure(IApplicationBuilder app)
        {
            var options = new RequestLocalizationOptions();

            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("it"),
                
            };
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.DefaultRequestCulture = new RequestCulture(
                culture: "en"
                , uiCulture: "en"
                );

            options.RequestCultureProviders = new[] {
                new RouteDataRequestCultureProvider()
                {
                    Options = options,
                    RouteDataStringKey = "language",
                    UIRouteDataStringKey = "language"
                }
            };
            app.UseRequestLocalization(options);
            
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDynamicControllerRoute<TranslationTransformer>(
                    "{language=en}/{controller=home}/{action=index}");
            });
        }
    }
}