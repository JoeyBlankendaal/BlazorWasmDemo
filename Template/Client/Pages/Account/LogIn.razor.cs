using Microsoft.AspNetCore.Components;
using Template.Client.Services;
using Template.Shared;
using Template.Shared.Parameters;

namespace Template.Client.Pages.Account;

public partial class LogIn
{
    public AccountLogInParameters Parameters = new();
    public Result Result { get; set; } = new();

    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    public UserService UserService { get; set; }

    public async Task OnValidSubmit()
    {
        try
        {
            await UserService.LogIn(Parameters);
            NavManager.NavigateTo("");
        }
        catch (Exception ex)
        {
            Result.HasSucceeded = false;
            Result.Messages = new[] { ex.Message };
        }
    }
}
