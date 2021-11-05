using System.Net;
using System.Net.Http.Json;
using Template.Shared.Parameters;

namespace Template.Client.Services;

public interface ICultureApi
{
    public Task SetCulture(CultureFormParameters parameters);
}

public class CultureApi : ICultureApi
{
    private readonly HttpClient _http;

    public CultureApi(HttpClient http)
    {
        _http = http;
    }

    public async Task SetCulture(CultureFormParameters parameters)
    {
        var result = await _http.PostAsJsonAsync("api/culture", parameters);

        if (result.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new Exception(await result.Content.ReadAsStringAsync());
        }

        result.EnsureSuccessStatusCode();
    }
}
