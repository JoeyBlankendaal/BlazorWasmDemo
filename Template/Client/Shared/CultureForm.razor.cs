using Microsoft.AspNetCore.Components;

namespace Template.Client.Shared;

public partial class CultureForm
{
    [CascadingParameter]
    public App App { get; set; }
}
