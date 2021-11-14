using Template.Server.Areas.Finance.Extensions;
using Template.Server.Areas.Identity.Extensions;
using Template.Server.Areas.Localization.Extensions;
using Template.Server.Extensions;
using Template.Server.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Host.AddConfiguration("appsettings.json");

var areas = config.GetSection("App:Areas").Get<string[]>();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbContext>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddRazorPages();

if (areas.Contains("Finance")) builder.Services.AddFinance();
if (areas.Contains("Identity")) builder.Services.AddIdentity(config);
if (areas.Contains("Localization")) builder.Services.AddLocalization(config);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDbContext(config);
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();

if (areas.Contains("Localization")) app.UseLocalization();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

if (areas.Contains("Identity")) app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

