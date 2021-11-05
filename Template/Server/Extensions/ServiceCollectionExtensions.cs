using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Template.Server.Services;
using Template.Shared.Models;
using Template.Shared.Services;

namespace Template.Server.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddLocalization(this IServiceCollection services, string[] cultures, string defaultCulture, string cookieName)
    {
        services.AddLocalization();
        services.AddScoped<Localizer>();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = cultures.Select(c => new CultureInfo(c)).ToArray();

            options.DefaultRequestCulture = new RequestCulture(defaultCulture);
            options.RequestCultureProviders = new[] { new CookieRequestCultureProvider { CookieName = cookieName } };
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

        var culture = new CultureInfo(defaultCulture);

        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }

    public static void AddUserService(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>().AddEntityFrameworkStores<DbContext>().AddDefaultTokenProviders();

        services.AddScoped<IUserEmailSender, UserEmailSender>();
        services.AddScoped<IUserService, UserService>();

        services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;

            #region Lockout
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = 10;
            options.Lockout.AllowedForNewUsers = true;
            #endregion

            #region Password
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            #endregion
        });

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;

            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            };
        });
    }
}
