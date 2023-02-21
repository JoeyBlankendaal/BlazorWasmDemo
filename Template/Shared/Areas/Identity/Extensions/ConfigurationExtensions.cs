using Microsoft.Extensions.Configuration;

namespace Template.Shared.Areas.Identity.Extensions;

public static class ConfigurationExtensions
{
    private const string _prefix = "Areas:Identity:";

    public static bool GetIdentityConfirmEmail(this IConfiguration config) =>
        config.GetValue<bool>(_prefix + "ConfirmEmail");
}
