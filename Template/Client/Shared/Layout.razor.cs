using Microsoft.AspNetCore.Components;
using Template.Client.Services;
using Template.Shared.Services;

namespace Template.Client.Shared;

public partial class Layout
{
    [Inject]
    public UserService UserService { get; set; }

    public async Task ResendEmailConfirmationUrl()
    {
        await UserService.ResendEmailConfirmationUrl();
    }
}
