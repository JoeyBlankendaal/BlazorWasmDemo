using Template.Server.Areas.Finance.Services;

namespace Template.Server.Areas.Finance.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddFinance(this IServiceCollection services)
    {
        services.AddScoped<IPortfolioService, PortfolioService>();
    }
}
