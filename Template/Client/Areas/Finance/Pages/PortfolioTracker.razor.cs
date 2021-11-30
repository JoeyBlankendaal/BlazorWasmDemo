using Microsoft.AspNetCore.Components;
using Template.Client.Areas.Finance.Services;
using Template.Client.Areas.Identity.Services;
using Template.Shared.Areas.Finance.Models;

namespace Template.Client.Areas.Finance.Pages;

public partial class PortfolioTracker
{
    private Portfolio CurrentPortfolio;
    private Portfolio[] Portfolios;

    private readonly Dictionary<AssetType, bool> AssetTypes = new()
    {
        [AssetType.Cryptocurrency] = true,
        [AssetType.Stock] = true
    };

    [Inject] private IPortfolioService PortfolioService { get; set; }
    [Inject] private UserService UserService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var user = UserService.GetCurrentUser();
        Portfolios = await PortfolioService.GetPortfolios(user);

        foreach (var portfolio in Portfolios)
        {
            portfolio.DisplayedAssets = portfolio.Assets;
        }

        if (Portfolios.Any())
        {
            SetPortfolio(Portfolios.FirstOrDefault().Id);
        }
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
