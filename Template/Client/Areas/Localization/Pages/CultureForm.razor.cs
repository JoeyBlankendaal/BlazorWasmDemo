using Microsoft.AspNetCore.Components;

namespace Template.Client.Areas.Localization.Pages;

public partial class CultureForm
{
    [CascadingParameter] private App App { get; set; }
}
