using Microsoft.AspNetCore.Identity;
using Template.Server.Services;
using Template.Shared.Models;

namespace Template.Server.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddUserService(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>().AddEntityFrameworkStores<DbContext>().AddDefaultTokenProviders();
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
