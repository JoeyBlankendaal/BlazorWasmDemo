using Microsoft.AspNetCore.Components;
using Template.Client.Areas.Identity.Services;
using Template.Shared.Areas.Identity.Parameters;
using Template.Shared.Models;

namespace Template.Client.Areas.Identity.Pages.Account;

public partial class ConfirmEmail
{
    private readonly AccountConfirmEmailParameters Parameters = new();
    private Result Result;

    [Inject] private NavigationManager NavManager { get; set; }
    [Inject] private UserService UserService { get; set; }

    [Parameter] public string UserId { get; set; }
    [Parameter] public string Token { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Parameters.UserId = UserId;
            Parameters.Token = Token;

            await UserService.ConfirmEmail(Parameters);
            NavManager.NavigateTo("");
        }
        catch (Exception ex)
        {
            Result = new Result
            {
                HasSucceeded = false,
                Messages = new[] { ex.Message }
            };
        }
    }
}
