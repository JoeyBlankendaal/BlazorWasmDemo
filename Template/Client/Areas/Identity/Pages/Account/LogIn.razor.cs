using Microsoft.AspNetCore.Components;
using Template.Client.Areas.Identity.Services;
using Template.Shared.Areas.Identity.Parameters;
using Template.Shared.Models;

namespace Template.Client.Areas.Identity.Pages.Account;

public partial class LogIn
{
    private bool Loading = false;
    private readonly AccountLogInParameters Parameters = new();
    private Result Result;

    [Inject] private NavigationManager NavManager { get; set; }
    [Inject] private UserService UserService { get; set; }

    private async Task OnValidSubmit()
    {
        try
        {
            Loading = true;
            await UserService.LogIn(Parameters);
            NavManager.NavigateTo("");
        }
        catch (Exception ex)
        {
            Loading = false;

            Result = new Result
            {
                HasSucceeded = false,
                Messages = new[] { ex.Message }
            };
        }
    }
}
