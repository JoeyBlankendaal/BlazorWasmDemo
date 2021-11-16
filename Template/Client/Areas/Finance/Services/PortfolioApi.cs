using System.Net.Http.Json;
using Template.Shared.Areas.Finance.Models;
using Template.Shared.Areas.Identity.Models;

namespace Template.Client.Areas.Finance.Services;

public interface IPortfolioApi
{
    public Task<Portfolio[]> GetPortfolios(User user);
}

public class PortfolioApi : IPortfolioApi
{
    private readonly HttpClient _http;

    public PortfolioApi(HttpClient http)
    {
        _http = http;
    }

    public async Task<Portfolio[]> GetPortfolios(User user)
    {
        return await _http.GetFromJsonAsync<Portfolio[]>("api/portfolios?user=" + user.Id);
    }
}
