using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Template.Shared.Areas.Localization.Services;

namespace Template.Server.Areas.Localization.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddLocalization(this IServiceCollection services, IConfiguration config)
    {
        // Get configuration properties
        var cookieName = config["App:CookieNames:Culture"];
        var cultures = config.GetSection("Areas:Localization:Cultures").Get<string[]>();
        var defaultCulture = config["Areas:Localization:DefaultCulture"];

        // Add services
        services.AddLocalization();
        services.AddScoped<Localizer>();

        // Configure options for RequestLocalizationMiddleware
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = cultures.Select(c => new CultureInfo(c)).ToArray();

            options.DefaultRequestCulture = new RequestCulture(defaultCulture);
            options.RequestCultureProviders = new[] { new CookieRequestCultureProvider { CookieName = cookieName } };
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

        var culture = new CultureInfo(defaultCulture);

        // Set the default culture for threads in the current application domain
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}
