using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Template.Client.Areas.Localization.Extensions;
using Template.Shared.Areas.Localization.Services;

namespace Template.Client.Areas.Localization.Components;

public class LocalizedDataAnnotationsValidator : ComponentBase
{
    [CascadingParameter] private EditContext CurrentEditContext { get; set; }
    [Inject] private Localizer Localizer { get; set; }

    protected override void OnInitialized()
    {
        CurrentEditContext.AddLocalizedDataAnnotationsValidation(Localizer);
    }
}
