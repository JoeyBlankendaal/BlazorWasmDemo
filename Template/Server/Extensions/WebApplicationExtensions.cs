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
}
