using Microsoft.EntityFrameworkCore;
using Template.Shared.Areas.Finance.Models;

namespace Template.Server.Areas.Finance.Services;

public interface IFinanceDbContext
{
    public DbSet<Cryptocurrency> Cryptocurrencies { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}

public class FinanceDbContext : IFinanceDbContext
{
    public DbSet<Cryptocurrency> Cryptocurrencies { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public static void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Cryptocurrency>(etb =>
        {
            etb.HasKey(c => c.Id);
        });

        builder.Entity<Currency>(etb =>
        {
            etb.HasKey(c => c.Id);
        });

        builder.Entity<Portfolio>(etb =>
        {
            etb.HasKey(p => p.Id);
        });

        builder.Entity<Stock>(etb =>
        {
            etb.HasKey(s => s.Id);
        });

        builder.Entity<Transaction>(etb =>
        {
            etb.HasKey(t => new { t.PortfolioId, t.AssetId });
        });
    }
}
