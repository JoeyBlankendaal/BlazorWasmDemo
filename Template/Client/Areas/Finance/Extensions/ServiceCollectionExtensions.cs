using Template.Client.Areas.Finance.Services;

namespace Template.Client.Areas.Finance.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddFinance(this IServiceCollection services)
    {
        services.AddScoped<IPortfolioApi, PortfolioApi>();
        services.AddScoped<IPortfolioService, PortfolioService>();
    }
}
