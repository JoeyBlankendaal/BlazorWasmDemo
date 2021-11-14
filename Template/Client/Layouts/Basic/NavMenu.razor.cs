using Microsoft.AspNetCore.Components;
using Template.Client.Areas.Identity.Services;

namespace Template.Client.Layouts.Basic;

public partial class NavMenu
{
    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    public UserService UserService { get; set; }

    public async Task LogOut()
    {
        await UserService.LogOut();
        NavManager.NavigateTo("");
    }
}
