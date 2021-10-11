using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;

namespace Template.Client.Extensions;

public static class WebAssemblyHostExtensions
{
    public static async Task SetDefaultCulture(this WebAssemblyHost host, string defaultCultureName)
    {
        CultureInfo culture;

        var js = host.Services.GetRequiredService<IJSRuntime>();
        var currentCultureName = await js.InvokeAsync<string>("blazorCulture.get");

        if (currentCultureName != null)
        {
            culture = new CultureInfo(currentCultureName);
        }
        else
        {
            culture = new CultureInfo(defaultCultureName);
            await js.InvokeVoidAsync("blazorCulture.set", defaultCultureName);
        }

        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        await host.RunAsync();
    }
}
