using Microsoft.EntityFrameworkCore;
using Template.Shared.Areas.Finance.Models;

namespace Template.Server.Areas.Finance.Services;

public interface IFinanceDbContext
{
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}

public class FinanceDbContext : IFinanceDbContext
{
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public static void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Asset>(etb =>
        {
            etb.HasKey(a => a.Id);

            etb.Property(a => a.Type).HasConversion(
                at => at.ToString(),
                at => (AssetType)Enum.Parse(typeof(AssetType), at));
        });

        builder.Entity<Portfolio>(etb =>
        {
            etb.HasKey(p => p.Id);
        });

        builder.Entity<Transaction>(etb =>
        {
            etb.HasKey(t => new { t.PortfolioId, t.AssetId });

            etb.Property(t => t.Type).HasConversion(
                tt => tt.ToString(),
                tt => (TransactionType)Enum.Parse(typeof(TransactionType), tt));
        });
    }
}
