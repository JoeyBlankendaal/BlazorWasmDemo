using Microsoft.AspNetCore.Components;
using Template.Client.Areas.Identity.Services;
using Template.Shared.Areas.Identity.Parameters;
using Template.Shared.Models;

namespace Template.Client.Areas.Identity.Pages.Account;

public partial class Create
{
    private AccountCreateParameters Parameters = new();
    private Result Result { get; set; }

    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    public UserService UserService { get; set; }

    private async Task OnValidSubmit()
    {
        try
        {
            await UserService.Create(Parameters);
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
