using Microsoft.Extensions.Options;
using Template.Server.Services;

namespace Template.Server.Extensions;

public static class WebApplicationExtensions
{
    public static void UseDbContext(this WebApplication app, bool recreate)
    {
        using var services = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var db = services.ServiceProvider.GetService<DbContext>();

        if (recreate)
        {
            db.Database.EnsureDeleted();
        }

        db.Database.EnsureCreated();
    }

    public static void UseLocalization(this WebApplication app)
    {
        var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
        app.UseRequestLocalization(options.Value);
    }
}
