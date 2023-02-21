using Template.Server.Services;
using Template.Shared.Extensions;

namespace Template.Server.Extensions;

public static class WebApplicationExtensions
{
    public static void UseDbContext(this WebApplication app, IConfiguration config)
    {
        // Get configuration properties
        var database = config.GetAppDatabase();
        var recreateOnRun = config.GetDatabaseRecreateOnRun(database);

        // Get DbContext service
        using var services = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var db = services.ServiceProvider.GetService<DbContext>();

        if (recreateOnRun)
        {
            db.Database.EnsureDeleted();
        }

        db.Database.EnsureCreated();
    }
}
