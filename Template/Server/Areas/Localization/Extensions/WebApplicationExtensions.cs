using Microsoft.Extensions.Options;

namespace Template.Server.Areas.Localization.Extensions;

public static class WebApplicationExtensions
{
    public static void UseLocalization(this WebApplication app)
    {
        // Use RequestLocalizationMiddleware with the options configured during the building of services
        app.UseRequestLocalization(app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value);
    }
}
