using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;
using Template.Shared.Areas.Localization.Extensions;

namespace Template.Client.Areas.Localization.Extensions;

public static class WebAssemblyHostExtensions
{
    public static async Task SetDefaultCulture(this WebAssemblyHost host, IConfiguration config)
    {
        CultureInfo culture;
        var js = host.Services.GetRequiredService<IJSRuntime>();

        // Get current and default cultures if they exist
        var currentCulture = await js.InvokeAsync<string>("blazorCulture.get");
        var defaultCulture = config.GetLocalizationDefaultCulture();

        // Check if a current culture is already set
        if (currentCulture != null)
        {
            culture = new CultureInfo(currentCulture);
        }
        else
        {
            culture = new CultureInfo(defaultCulture);

            // Set Blazor culture as default culture
            await js.InvokeVoidAsync("blazorCulture.set", defaultCulture);
        }

        // Set the default culture for threads in the current application domain
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        await host.RunAsync();
    }
}
