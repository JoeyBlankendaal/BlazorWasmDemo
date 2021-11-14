namespace Template.Shared.Areas.Finance.Models;

public class Asset
{
    public Guid Id { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public double MarketCapitalization { get; set; }
}
