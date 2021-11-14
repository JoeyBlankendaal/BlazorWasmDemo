namespace Template.Shared.Areas.Finance.Models;

public class Transaction
{
    public Guid PortfolioId { get; set; }
    public Guid AssetId { get; set; }
    public string Type { get; set; } // Buy, Sell, Transfer In, Transfer Out
    public double Quantity { get; set; }
    public double Price { get; set; }
    public DateTime DateTime { get; set; }
    public double Fee { get; set; }
    public string Notes { get; set; }
}
