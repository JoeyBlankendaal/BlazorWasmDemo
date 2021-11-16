namespace Template.Shared.Areas.Finance.Models;

public enum TransactionType
{
    Buy,
    Sell,
    TransferIn,
    TransferOut
}

public class Transaction
{
    public Guid PortfolioId { get; set; }
    public Guid AssetId { get; set; }
    public TransactionType Type { get; set; }
    public double Quantity { get; set; }
    public double Price { get; set; }
    public DateTime DateTime { get; set; }
    public double Fee { get; set; }
    public string Notes { get; set; }
}
