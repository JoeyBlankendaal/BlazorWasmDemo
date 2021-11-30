using Microsoft.AspNetCore.Components;
using Template.Client.Areas.Identity.Services;
using Template.Shared.Areas.Identity.Parameters;
using Template.Shared.Models;

namespace Template.Client.Areas.Identity.Pages.Settings;

public partial class Password
{
    private bool Loading = false;
    private readonly SettingsPasswordParameters Parameters = new();
    private Result Result;

    [Inject] private UserService UserService { get; set; }

    protected override void OnInitialized()
    {
        Parameters.UserId = UserService.GetCurrentUser().Id.ToString();
    }

    private async Task OnValidSubmit()
    {
        try
        {
            Loading = true;
            await UserService.SetPassword(Parameters);
            Loading = false;

            Result = new Result
            {
                HasSucceeded = true,
                Messages = new[] { Localizer["YourPasswordHasBeenChanged"] }
            };
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
