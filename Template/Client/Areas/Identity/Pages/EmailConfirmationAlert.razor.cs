using Microsoft.AspNetCore.Components;
using Template.Client.Areas.Identity.Services;
using Template.Shared.Areas.Identity.Models;
using Template.Shared.Areas.Identity.Parameters;

namespace Template.Client.Areas.Identity.Pages;

public partial class EmailConfirmationAlert
{
    private readonly ResendEmailConfirmationUrlParameters Parameters = new();
    private User User;

    [Inject] private UserService UserService { get; set; }

    protected override void OnInitialized()
    {
        User = UserService.GetCurrentUser();
        Parameters.UserId = User.Id.ToString();
    }

    private async Task ResendEmailConfirmationUrl()
    {
        await UserService.ResendEmailConfirmationUrl(Parameters);
    }
}
