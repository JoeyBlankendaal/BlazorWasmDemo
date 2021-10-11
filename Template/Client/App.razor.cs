using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace Template.Client;

public partial class App
{
    [Inject]
    public IJSRuntime JS { get; set; }

    [Inject]
    public NavigationManager Nav { get; set; }

    public CultureInfo[] Cultures { get; set; }

    public CultureInfo Culture
    {
        get => CultureInfo.CurrentCulture;
        set
        {
            if (CultureInfo.CurrentCulture != value)
            {
                ((IJSInProcessRuntime)JS).InvokeVoid("blazorCulture.set", value.Name);
                Nav.NavigateTo(Nav.Uri, forceLoad: true);
            }
        }
    }

    protected override void OnInitialized()
    {
        Cultures = Array.ConvertAll(Config.GetSection("App:Cultures").Get<string[]>(), c => new CultureInfo(c));
    }
}
