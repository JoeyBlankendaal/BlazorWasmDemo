using Microsoft.AspNetCore.Components.Authorization;
using Template.Client.Services;

namespace Template.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddUserService(this IServiceCollection services)
    {
        services.AddOptions();
        services.AddAuthorizationCore();
        services.AddScoped<UserService>();
        services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<UserService>());
        services.AddScoped<IUserApi, UserApi>();
    }
}
