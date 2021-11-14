using Template.Server.Services;

namespace Template.Server.Extensions;

public static class WebApplicationExtensions
{
    public static void UseDbContext(this WebApplication app, IConfiguration config)
    {
        // Get configuration property
        var recreate = config.GetValue<bool>("Db:Recreate");

        // Get DbContext service
        using var services = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var db = services.ServiceProvider.GetService<DbContext>();

        if (recreate)
        {
            db.Database.EnsureDeleted();
        }

        db.Database.EnsureCreated();
    }
}
