using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Template.Client.Extensions;
using Template.Shared.Services;

namespace Template.Client.Components;

public class LocalizedDataAnnotationsValidator : ComponentBase
{
    [CascadingParameter]
    private EditContext CurrentEditContext { get; set; }

    [Inject]
    private Localizer Localizer { get; set; }

    protected override void OnInitialized()
    {
        CurrentEditContext.AddLocalizedDataAnnotationsValidation(Localizer);
    }
}
