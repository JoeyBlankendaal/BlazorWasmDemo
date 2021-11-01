using Microsoft.AspNetCore.Components;
using Template.Client.Services;
using Template.Shared;
using Template.Shared.Parameters;

namespace Template.Client.Pages.Account;

public partial class ConfirmEmail
{
    public AccountConfirmEmailParameters Parameters = new();
    public Result Result { get; set; }

    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    public UserService UserService { get; set; }

    [Parameter]
    public string Id { get; set; }

    [Parameter]
    public string Token { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Parameters.Id = Id;
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
