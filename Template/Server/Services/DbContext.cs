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
    private readonly string[] _areas;
    private readonly string _connectionString;

    #region Finance
    public DbSet<Cryptocurrency> Cryptocurrencies { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    #endregion

    public DbContext(IConfiguration config)
    {
        // Get configuration properties
        _areas = config.GetSection("App:Areas").Get<string[]>();
        _connectionString = config["Db:ConnectionString"];
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlite(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        if (_areas.Contains("Finance")) FinanceDbContext.OnModelCreating(builder);
        if (_areas.Contains("Identity")) IdentityDbContext.OnModelCreating(builder);
    }
}
