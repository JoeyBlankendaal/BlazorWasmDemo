using Microsoft.Extensions.Localization;

namespace Template.Shared.Services;

public class Localizer
{
    private readonly IStringLocalizer<Resources.Resources> _localizer;

    // Return a value string instead of a LocalizedString, which is passed as JSON to the Client.
    public string this[string name] => _localizer[name].Value;

    public Localizer(IStringLocalizer<Resources.Resources> localizer)
    {
        _localizer = localizer;
    }
}
