using System.Net;
using System.Net.Http.Json;
using Template.Shared.Models;
using Template.Shared.Parameters;

namespace Template.Client.Services;

public interface IUserApi
{
    public Task ConfirmEmail(AccountConfirmEmailParameters parameters);
    public Task Create(AccountCreateParameters parameters);
    public Task<UserInfo> GetUserInfo();
    public Task LogIn(AccountLogInParameters parameters);
    public Task LogOut();
    public Task ResendEmailConfirmationUrl();
    public Task SetPassword(SettingsPasswordParameters parameters);
}

public class UserApi : IUserApi
{
    private readonly HttpClient _http;

    public UserApi(HttpClient http)
    {
        _http = http;
    }

    public async Task ConfirmEmail(AccountConfirmEmailParameters parameters)
    {
        var result = await _http.PostAsJsonAsync("api/user/confirm-email", parameters);

        if (result.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new Exception(await result.Content.ReadAsStringAsync());
        }

        result.EnsureSuccessStatusCode();
    }

    public async Task Create(AccountCreateParameters parameters)
    {
        var response = await _http.PostAsJsonAsync("api/user/create", parameters);

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        response.EnsureSuccessStatusCode();
    }

    public async Task<UserInfo> GetUserInfo()
    {
        return await _http.GetFromJsonAsync<UserInfo>("api/user/info");
    }

    public async Task LogIn(AccountLogInParameters parameters)
    {
        var response = await _http.PostAsJsonAsync("api/user/log-in", parameters);

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        response.EnsureSuccessStatusCode();
    }

    public async Task LogOut()
    {
        var response = await _http.DeleteAsync("api/user/log-out");
        response.EnsureSuccessStatusCode();
    }

    public async Task ResendEmailConfirmationUrl()
    {
        var result = await _http.PutAsJsonAsync("api/user/resend-email-confirmation-url", new EmptyParameters());

        if (result.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new Exception(await result.Content.ReadAsStringAsync());
        }

        result.EnsureSuccessStatusCode();
    }

    public async Task SetPassword(SettingsPasswordParameters parameters)
    {
        var result = await _http.PutAsJsonAsync("api/user/password", parameters);

        if (result.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new Exception(await result.Content.ReadAsStringAsync());
        }

        result.EnsureSuccessStatusCode();
    }
}
