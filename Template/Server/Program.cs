using Template.Server.Areas.Finance.Extensions;
using Template.Server.Areas.Identity.Extensions;
using Template.Server.Areas.Localization.Extensions;
using Template.Server.Extensions;
using Template.Server.Services;
using Template.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
var config = builder.Configuration;
config.AddConfiguration("appsettings.json");

// Add core services
var services = builder.Services;
services.AddControllersWithViews();
services.AddDbContext<DbContext>();
services.AddScoped<IEmailSender, EmailSender>();
services.AddRazorPages();

// Add area services
var areas = config.GetAppAreas();
if (areas.Contains("Finance")) services.AddFinance();
if (areas.Contains("Identity")) services.AddIdentity(config);
if (areas.Contains("Localization")) services.AddLocalization(config);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Add development middleware
    app.UseDbContext(config);
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}
else
{
    // Add production and staging middleware
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

// Add universal middleware
app.UseHttpsRedirection();
if (areas.Contains("Localization")) app.UseLocalization();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
if (areas.Contains("Identity")) app.UseAuthentication();
app.UseAuthorization();

// Add endpoints
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
