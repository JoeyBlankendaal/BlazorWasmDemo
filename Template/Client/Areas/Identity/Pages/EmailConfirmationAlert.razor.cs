using Microsoft.AspNetCore.Components;
using Template.Client.Areas.Identity.Services;
using Template.Shared.Areas.Identity.Models;
using Template.Shared.Areas.Identity.Parameters;

namespace Template.Client.Areas.Identity.Pages;

public partial class EmailConfirmationAlert
{
    public ResendEmailConfirmationUrlParameters Parameters = new();
    public User User { get; set; }

    [Inject]
    public UserService UserService { get; set; }

    protected override void OnInitialized()
    {
        User = UserService.GetCurrentUser();
    }

    public async Task ResendEmailConfirmationUrl()
    {
        await UserService.ResendEmailConfirmationUrl(Parameters);
    }
}
