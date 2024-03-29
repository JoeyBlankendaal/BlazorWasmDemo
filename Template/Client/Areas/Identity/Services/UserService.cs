﻿using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Template.Shared.Areas.Identity.Models;
using Template.Shared.Areas.Identity.Parameters;

namespace Template.Client.Areas.Identity.Services;

public class UserService : AuthenticationStateProvider
{
    private readonly IUserApi _userApi;
    private UserInfo _userInfo;

    public UserService(IUserApi userApi)
    {
        _userApi = userApi;
    }

    public async Task ConfirmEmail(AccountConfirmEmailParameters parameters)
    {
        await _userApi.ConfirmEmail(parameters);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task Create(AccountCreateParameters parameters)
    {
        await _userApi.Create(parameters);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public User GetCurrentUser() => _userInfo.CurrentUser;

    private async Task<UserInfo> GetUserInfo()
    {
        if (_userInfo != null && _userInfo.IsAuthenticated)
        {
            return _userInfo;
        }

        _userInfo = await _userApi.GetUserInfo();
        return _userInfo;
    }

    public async Task LogIn(AccountLogInParameters parameters)
    {
        await _userApi.LogIn(parameters);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogOut()
    {
        await _userApi.LogOut();
        _userInfo = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task ResendEmailConfirmationUrl(ResendEmailConfirmationUrlParameters parameters)
    {
        await _userApi.ResendEmailConfirmationUrl(parameters);
    }

    public async Task SetPassword(SettingsPasswordParameters parameters)
    {
        await _userApi.SetPassword(parameters);
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();

        try
        {
            var userInfo = await GetUserInfo();

            if (userInfo.IsAuthenticated)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, userInfo.CurrentUser.Id.ToString())
                }
                .Concat(userInfo.ExposedClaims.Select(c => new Claim(c.Key, c.Value)));

                identity = new ClaimsIdentity(claims, "User");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Request failed: " + ex.ToString());
        }

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }
}
