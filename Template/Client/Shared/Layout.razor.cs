using Microsoft.AspNetCore.Components;
using Template.Client.Services;

namespace Template.Client.Shared;

public partial class Layout
{
    [Inject]
    public UserService UserService { get; set; }
}
