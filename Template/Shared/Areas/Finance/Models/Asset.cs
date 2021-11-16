using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Shared.Areas.Finance.Models;

public enum AssetType
{
    Cryptocurrency,
    Stock
}

public class Asset
{
    public Guid Id { get; set; }
    public AssetType Type { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public double MarketCapitalization { get; set; }

    [NotMapped]
    public Transaction[] Transactions { get; set; }

    [NotMapped]
    public double AverageBuyPrice { get; set; }

    [NotMapped]
    public double HoldingsQuantity { get; set; }

    [NotMapped]
    public double HoldingsValue { get; set; }

    [NotMapped]
    public double ProfitOrLossPercentage { get; set; }

    [NotMapped]
    public double ProfitOrLossValue { get; set; }
}
