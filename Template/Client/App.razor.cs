using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;
using Template.Client.Services;
using Template.Shared.Parameters;

namespace Template.Client;

public partial class App
{
    [Inject]
    public ICultureApi CultureApi { get; set; }

    [Inject]
    public IJSRuntime JS { get; set; }

    [Inject]
    public NavigationManager NavManager { get; set; }

    public CultureInfo[] Cultures { get; set; }

    public CultureInfo Culture
    {
        get => CultureInfo.CurrentCulture;
        set
        {
            if (CultureInfo.CurrentCulture != value)
            {
                ((IJSInProcessRuntime)JS).InvokeVoid("blazorCulture.set", value.Name);
                NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
            }
        }
    }

    public async Task SetCulture(ChangeEventArgs args)
    {
        var name = args.Value.ToString();

        // Set culture on client
        Culture = new CultureInfo(name);

        // Pass culture name to server
        await CultureApi.SetCulture(new CultureFormParameters { CultureName = name });
    }

    protected override void OnInitialized()
    {
        Cultures = Array.ConvertAll(Config.GetSection("App:Cultures").Get<string[]>(), c => new CultureInfo(c));
    }
}
