using Microsoft.EntityFrameworkCore;
using Template.Server.Areas.Finance.Services;
using Template.Server.Areas.Identity.Services;
using Template.Shared.Areas.Finance.Models;
using Template.Shared.Areas.Identity.Models;
using EntityFramework = Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Template.Server.Services;

public class DbContext :
    EntityFramework.IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>,
    IFinanceDbContext,
    IIdentityDbContext
{
    private readonly IConfiguration _config;

    #region Finance
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    #endregion

    public DbContext(IConfiguration config)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Get configuration properties
        var database = _config["App:Database"];
        var dbms = _config[$"Databases:{database}:DBMS"];
        var connectionString = _config[$"Databases:{database}:ConnectionString"];

        // Configure the context to connect to a database with a configured type
        switch (dbms)
        {
            case "MySQL":
                builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                break;
            case "SQLite":
                builder.UseSqlite(connectionString);
                break;
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var areas = _config.GetSection("App:Areas").Get<string[]>();

        if (areas.Contains("Finance")) FinanceDbContext.OnModelCreating(builder);
        if (areas.Contains("Identity")) IdentityDbContext.OnModelCreating(builder);
    }
}
