using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;
using Template.Shared.Areas.Localization.Extensions;
using Template.Shared.Areas.Localization.Parameters;

namespace Template.Client.Areas.Localization.Services;

public interface ICultureService
{
    public CultureInfo GetCurrentCulture();
    public CultureInfo[] GetSupportedCultures();
    public Task SetCurrentCulture(ChangeEventArgs args);
}

public class CultureService : ICultureService
{
    private readonly ICultureApi _cultureApi;
    private readonly IJSRuntime _js;
    private readonly NavigationManager _navManager;
    private readonly CultureInfo[] _supportedCultures;

    public CultureService(IConfiguration config, ICultureApi cultureApi, IJSRuntime js, NavigationManager navManager)
    {
        _cultureApi = cultureApi;
        _js = js;
        _navManager = navManager;
        _supportedCultures = Array.ConvertAll(config.GetLocalizationCultures(), c => new CultureInfo(c));
    }

    public CultureInfo GetCurrentCulture()
    {
        return CultureInfo.CurrentCulture;
    }

    public CultureInfo[] GetSupportedCultures()
    {
        return _supportedCultures;
    }

    public async Task SetCurrentCulture(ChangeEventArgs args)
    {
        var name = args.Value.ToString();

        // Pass culture name to the Server
        await _cultureApi.SetCulture(new CultureFormParameters { CultureName = name });

        // Set culture in the Client
        var culture = new CultureInfo(name);

        if (CultureInfo.CurrentCulture != culture)
        {
            ((IJSInProcessRuntime)_js).InvokeVoid("blazorCulture.set", name);
            _navManager.NavigateTo(_navManager.Uri, forceLoad: true);
        }
    }
}
