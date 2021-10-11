using Microsoft.AspNetCore.Components;
using Template.Client.Services;

namespace Template.Client.Shared;

public partial class Nav
{
    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    public UserService UserService { get; set; }

    public async Task OnClickLogOut()
    {
        await UserService.LogOut();
        NavManager.NavigateTo("");
    }
}
