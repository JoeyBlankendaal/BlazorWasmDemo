using Microsoft.AspNetCore.Components;
using Template.Client.Areas.Localization.Services;

namespace Template.Client.Areas.Localization.Pages;

public partial class CultureForm
{
    [Inject] private ICultureService CultureService { get; set; }
}
