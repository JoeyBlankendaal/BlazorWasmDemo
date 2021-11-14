using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;

namespace Template.Client.Areas.Localization.Extensions;

public static class WebAssemblyHostExtensions
{
    public static async Task SetDefaultCulture(this WebAssemblyHost host, IConfiguration config)
    {
        CultureInfo culture;
        var js = host.Services.GetRequiredService<IJSRuntime>();

        // Get current and default cultures' names
        var currentCultureName = await js.InvokeAsync<string>("blazorCulture.get");
        var defaultCultureName = config["App:DefaultCulture"];

        // Check if a current culture is already set
        if (currentCultureName != null)
        {
            culture = new CultureInfo(currentCultureName);
        }
        else
        {
            culture = new CultureInfo(defaultCultureName);

            // Set Blazor culture as default culture
            await js.InvokeVoidAsync("blazorCulture.set", defaultCultureName);
        }

        // Set the default culture for threads in the current application domain
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        await host.RunAsync();
    }
}
