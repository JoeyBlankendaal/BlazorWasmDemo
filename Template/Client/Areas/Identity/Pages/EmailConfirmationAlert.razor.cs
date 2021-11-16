using Microsoft.AspNetCore.Components;
using Template.Client.Areas.Identity.Services;
using Template.Shared.Areas.Identity.Models;
using Template.Shared.Areas.Identity.Parameters;

namespace Template.Client.Areas.Identity.Pages;

public partial class EmailConfirmationAlert
{
    private ResendEmailConfirmationUrlParameters Parameters = new();
    private User User { get; set; }

    [Inject]
    public UserService UserService { get; set; }

    protected override void OnInitialized()
    {
        User = UserService.GetCurrentUser();
    }

    private async Task ResendEmailConfirmationUrl()
    {
        await UserService.ResendEmailConfirmationUrl(Parameters);
    }
}
