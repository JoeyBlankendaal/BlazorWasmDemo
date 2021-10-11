using Blankendaal.Template.Server.Extensions;
using Template.Server.Extensions;
using Template.Server.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Host.AddConfiguration("appsettings.json");

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbContext>();
builder.Services.AddRazorPages();
builder.Services.AddUserService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDbContext(config.GetValue<bool>("Db:Recreate"));
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

