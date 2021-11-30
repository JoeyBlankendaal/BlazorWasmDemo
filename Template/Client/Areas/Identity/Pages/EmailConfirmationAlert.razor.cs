using Microsoft.AspNetCore.Components;
using Template.Client.Areas.Identity.Services;
using Template.Shared.Areas.Identity.Models;
using Template.Shared.Areas.Identity.Parameters;

namespace Template.Client.Areas.Identity.Pages;

public enum State
{
    Unsubmitted,
    Loading,
    Submitted
}

public partial class EmailConfirmationAlert
{
    private readonly ResendEmailConfirmationUrlParameters Parameters = new();
    private State State = State.Unsubmitted;
    private User User;

    [Inject] private UserService UserService { get; set; }

    protected override void OnInitialized()
    {
        User = UserService.GetCurrentUser();
        Parameters.UserId = User.Id.ToString();
    }

    private async Task ResendEmailConfirmationUrl()
    {
        State = State.Loading;
        await UserService.ResendEmailConfirmationUrl(Parameters);
        State = State.Submitted;
    }
}
