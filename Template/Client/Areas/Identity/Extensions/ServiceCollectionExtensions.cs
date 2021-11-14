using Microsoft.AspNetCore.Components.Authorization;
using Template.Client.Areas.Identity.Services;

namespace Template.Client.Areas.Identity.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddOptions();
        services.AddAuthorizationCore();
        services.AddScoped<UserService>();
        services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<UserService>());
        services.AddScoped<IUserApi, UserApi>();
    }
}
