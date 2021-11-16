using Template.Shared.Areas.Finance.Models;
using Template.Shared.Areas.Identity.Models;

namespace Template.Client.Areas.Finance.Services;

public interface IPortfolioService
{
    public Task<Portfolio[]> GetPortfolios(User user);
}

public class PortfolioService : IPortfolioService
{
    private readonly IPortfolioApi _portfolioApi;

    public PortfolioService(IPortfolioApi portfolioApi)
    {
        _portfolioApi = portfolioApi;
    }

    public async Task<Portfolio[]> GetPortfolios(User user)
    {
        return await _portfolioApi.GetPortfolios(user);
    }
}
