using Microsoft.Extensions.Configuration;

namespace Template.Shared.Areas.Localization.Extensions;

public static class ConfigurationExtensions
{
    private const string _prefix = "Areas:Localization:";

    public static string GetLocalizationCookieName(this IConfiguration config) => config[_prefix + "CookieName"];

    public static string[] GetLocalizationCultures(this IConfiguration config) =>
        config.GetSection(_prefix + "Cultures").Get<string[]>();

    public static string GetLocalizationDefaultCulture(this IConfiguration config) =>
        config[_prefix + "DefaultCulture"];
}
