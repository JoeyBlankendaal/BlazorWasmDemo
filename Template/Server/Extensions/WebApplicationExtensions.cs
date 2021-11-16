using Template.Server.Services;

namespace Template.Server.Extensions;

public static class WebApplicationExtensions
{
    public static void UseDbContext(this WebApplication app, IConfiguration config)
    {
        // Get configuration properties
        var database = config["App:Database"];
        var recreateOnRun = config.GetValue<bool>($"Database:{database}:RecreateOnRun");

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
