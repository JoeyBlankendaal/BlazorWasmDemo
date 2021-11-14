using Microsoft.AspNetCore.Components;

namespace Template.Client.Areas.Localization.Pages;

public partial class CultureForm
{
    [CascadingParameter]
    public App App { get; set; }
}
