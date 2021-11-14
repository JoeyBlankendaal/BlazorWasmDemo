using Microsoft.AspNetCore.Components;
using Template.Client.Areas.Identity.Services;
using Template.Shared.Areas.Identity.Parameters;
using Template.Shared.Models;

namespace Template.Client.Areas.Identity.Pages.Settings;

public partial class Password
{
    public SettingsPasswordParameters Parameters = new();
    public Result Result { get; set; }

    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    public UserService UserService { get; set; }

    public async Task OnValidSubmit()
    {
        try
        {
            await UserService.SetPassword(Parameters);

            Result = new Result
            {
                HasSucceeded = true,
                Messages = new[] { "YourPasswordHasBeenChanged" }
            };
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
