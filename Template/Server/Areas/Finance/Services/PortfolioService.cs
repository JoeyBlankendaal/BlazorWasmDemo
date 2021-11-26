using Template.Server.Services;
using Template.Shared.Areas.Finance.Models;
using Template.Shared.Areas.Identity.Models;

namespace Template.Server.Areas.Finance.Services;

public interface IPortfolioService
{
    public Portfolio[] GetPortfolios(User user);
}

public class PortfolioService : IPortfolioService
{
    private readonly DbContext _db;

    public PortfolioService(DbContext db)
    {
        _db = db;
    }

    public Portfolio[] GetPortfolios(User user)
    {
        // Get all user portfolios
        var portfolios = _db.Portfolios.Where(p => p.UserId == user.Id).ToArray();

        // Get all assets at once to prevent multiple queries
        var assets = _db.Assets.ToArray();

        foreach (var portfolio in portfolios)
        {
            var portfolioAssets = new List<Asset>();

            // Get all portfolio transactions
            var transactions = _db.Transactions.Where(t => t.PortfolioId == portfolio.Id).ToArray();

            // Iterate through transactions to get portfolio assets
            foreach (var transaction in transactions)
            {
                var asset = assets.FirstOrDefault(a => a.Id == transaction.AssetId);

                // Check if no transactions with asset exist
                if (asset == null)
                {
                    continue;
                }

                portfolioAssets.Add(asset);
            }

            // Start tracking portfolio profit/loss
            var averageBuyValue = 0.0;
            var profitOrLossValue = 0.0;

            // Iterate through portfolio assets to get portfolio transactions
            foreach (var asset in portfolioAssets)
            {
                asset.Transactions = transactions.Where(t => t.AssetId == asset.Id).ToArray();

                // Get derivative properties, in this particular order
                asset.AverageBuyPrice = GetAverageBuyPrice(asset);
                asset.HoldingsQuantity = GetHoldingsQuantity(asset);
                asset.HoldingsValue = GetHoldingsValue(asset); // Requires HoldingsQuantity
                asset.ProfitOrLossValue = GetProfitOrLossValue(asset);
                asset.ProfitOrLossPercentage = GetProfitOrLossPercentage(asset); // Requires ProfitsOrLossValue, HoldingsQuantity, and AverageBuyPrice

                averageBuyValue += asset.HoldingsQuantity * asset.AverageBuyPrice;
                profitOrLossValue += asset.ProfitOrLossValue;
            }

            portfolio.Assets = portfolioAssets.ToArray();

            // Calculate portfolio profit/loss
            portfolio.ProfitOrLossValue = profitOrLossValue;

            portfolio.ProfitOrLossPercentage = profitOrLossValue != 0 && averageBuyValue != 0
                ? (profitOrLossValue / averageBuyValue * 100) - 100
                : 0;
        }

        return portfolios;
    }

    private static double GetAverageBuyPrice(Asset asset)
    {
        var price = 0.0;
        var quantity = 0.0;

        foreach (var transaction in asset.Transactions)
        {
            price = transaction.Quantity * transaction.Price;
            quantity = transaction.Quantity;
        }

        return price / quantity;
    }

    private static double GetHoldingsQuantity(Asset asset)
    {
        var quantity = 0.0;

        foreach (var transaction in asset.Transactions)
        {
            if (new[] { TransactionType.Buy, TransactionType.TransferIn }.Contains(transaction.Type))
            {
                quantity += transaction.Quantity;
            }
            else if (new[] { TransactionType.Sell, TransactionType.TransferOut }.Contains(transaction.Type))
            {
                quantity -= transaction.Quantity;
            }
        }

        return quantity;
    }

    private static double GetHoldingsValue(Asset asset)
    {
        return asset.HoldingsQuantity * asset.Price;
    }

    private static double GetProfitOrLossPercentage(Asset asset)
    {
        var averageBuyValue = asset.HoldingsQuantity * asset.AverageBuyPrice;
        return (asset.ProfitOrLossValue / averageBuyValue * 100) - 100;
    }

    private static double GetProfitOrLossValue(Asset asset)
    {
        var value = 0.0;

        foreach (var transaction in asset.Transactions)
        {
            value += (asset.Price - transaction.Price) * transaction.Quantity;
        }

        return value;
    }
}
