using System.Net.Http.Json;
using Template.Shared.Areas.Finance.Models;

namespace Template.Client.Areas.Finance.Services;

public interface IPortfolioApi
{
    public Task<Portfolio[]> GetPortfolios();
}

public class PortfolioApi : IPortfolioApi
{
    private readonly HttpClient _http;

    public PortfolioApi(HttpClient http)
    {
        _http = http;
    }

    public async Task<Portfolio[]> GetPortfolios()
    {
        return await _http.GetFromJsonAsync<Portfolio[]>("api/portfolios");
    }
}
