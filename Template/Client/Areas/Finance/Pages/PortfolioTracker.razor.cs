using Microsoft.AspNetCore.Components;
using Template.Client.Areas.Finance.Services;
using Template.Client.Areas.Identity.Services;
using Template.Shared.Areas.Finance.Models;

namespace Template.Client.Areas.Finance.Pages;

public partial class PortfolioTracker
{
    private Portfolio CurrentPortfolio { get; set; }
    private Portfolio[] Portfolios { get; set; }

    [Inject]
    public IPortfolioService PortfolioService { get; set; }

    [Inject]
    public UserService UserService { get; set; }

    private Dictionary<AssetType, bool> AssetTypes = new()
    {
        [AssetType.Cryptocurrency] = true,
        [AssetType.Stock] = true
    };

    protected override async Task OnInitializedAsync()
    {
        var user = UserService.GetCurrentUser();
        Portfolios = await PortfolioService.GetPortfolios(user);

        foreach (var portfolio in Portfolios)
        {
            portfolio.DisplayedAssets = portfolio.Assets;
        }

        SetPortfolio(Portfolios.FirstOrDefault().Id);
    }

    private void SetPortfolio(ChangeEventArgs args)
    {
        var id = Guid.Parse(args.Value.ToString());
        SetPortfolio(id);
    }

    private void SetPortfolio(Guid id)
    {
        CurrentPortfolio = Portfolios.FirstOrDefault(p => p.Id == id);
    }
}
