using Template.Shared.Areas.Finance.Models;

namespace Template.Client.Areas.Finance.Services;

public interface IPortfolioService
{
    public Task<Portfolio[]> GetPortfolios();
}

public class PortfolioService : IPortfolioService
{
    private readonly IPortfolioApi _portfolioApi;

    public PortfolioService(IPortfolioApi portfolioApi)
    {
        _portfolioApi = portfolioApi;
    }

    public async Task<Portfolio[]> GetPortfolios()
    {
        return await _portfolioApi.GetPortfolios();
    }
}
