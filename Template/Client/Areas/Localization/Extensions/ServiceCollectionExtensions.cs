using Template.Client.Areas.Localization.Services;
using Template.Shared.Areas.Localization.Services;

namespace Template.Client.Areas.Localization.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddLocalization(this IServiceCollection services, IConfiguration config)
    {
        services.AddLocalization();
        services.AddScoped<Localizer>();
        services.AddScoped<ICultureApi, CultureApi>();
    }
}
