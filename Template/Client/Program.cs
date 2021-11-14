using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Template.Client;
using Template.Client.Areas.Finance.Extensions;
using Template.Client.Areas.Identity.Extensions;
using Template.Client.Areas.Localization.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var config = builder.Configuration;
var areas = config.GetSection("App:Areas").Get<string[]>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

if (areas.Contains("Finance")) builder.Services.AddFinance();
if (areas.Contains("Identity")) builder.Services.AddIdentity();
if (areas.Contains("Localization")) builder.Services.AddLocalization(config);

var host = builder.Build();

if (areas.Contains("Localization")) await host.SetDefaultCulture(config);

await host.RunAsync();
