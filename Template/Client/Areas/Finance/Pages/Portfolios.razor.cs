using Microsoft.AspNetCore.Components;
using Template.Client.Areas.Finance.Services;
using Template.Shared.Areas.Finance.Models;

namespace Template.Client.Areas.Finance.Pages;

public partial class Portfolios
{
    public Portfolio CurrentPortfolio { get; set; }
    public Portfolio[] AvailablePortfolios { get; set; }

    [Inject]
    public IPortfolioService PortfolioService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        AvailablePortfolios = await PortfolioService.GetPortfolios();
    }

    public void SetPortfolio(ChangeEventArgs args)
    {
        var id = Guid.Parse(args.Value.ToString());
        CurrentPortfolio = AvailablePortfolios.FirstOrDefault(p => p.Id == id);
    }
}
