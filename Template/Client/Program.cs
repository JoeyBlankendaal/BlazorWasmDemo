using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Template.Client;
using Template.Client.Areas.Finance.Extensions;
using Template.Client.Areas.Identity.Extensions;
using Template.Client.Areas.Localization.Extensions;
using Template.Shared.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var config = builder.Configuration;

// Add root components
var rootComponents = builder.RootComponents;
rootComponents.Add<App>("#app");
rootComponents.Add<HeadOutlet>("head::after");

// Add core services
var services = builder.Services;
services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add area services
var areas = config.GetAppAreas();
if (areas.Contains("Finance")) builder.Services.AddFinance();
if (areas.Contains("Identity")) builder.Services.AddIdentity();
if (areas.Contains("Localization")) builder.Services.AddLocalization(config);

// Build and run with host
var host = builder.Build();
if (areas.Contains("Localization")) await host.SetDefaultCulture(config);
await host.RunAsync();
