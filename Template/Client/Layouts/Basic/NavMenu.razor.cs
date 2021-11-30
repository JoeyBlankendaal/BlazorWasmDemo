using Microsoft.AspNetCore.Components;
using Template.Client.Areas.Identity.Services;

namespace Template.Client.Layouts.Basic;

public partial class NavMenu
{
    [Inject] private NavigationManager NavManager { get; set; }
    [Inject] private UserService UserService { get; set; }

    protected async Task LogOut()
    {
        await UserService.LogOut();
        NavManager.NavigateTo("");
    }
}
