using Microsoft.AspNetCore.Components;
using Template.Client.Services;
using Template.Shared;
using Template.Shared.Parameters;

namespace Template.Client.Pages.Account;

public partial class Create
{
    public AccountCreateParameters Parameters = new();
    public Result Result { get; set; }

    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    public UserService UserService { get; set; }

    public async Task OnValidSubmit()
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
