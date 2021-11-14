using Template.Server.Services;
using Template.Shared.Areas.Finance.Models;

namespace Template.Server.Areas.Finance.Services;

public interface IPortfolioService
{
    public Portfolio[] GetPortfolios();
}

public class PortfolioService : IPortfolioService
{
    private readonly DbContext _db;

    public PortfolioService(DbContext db)
    {
        _db = db;
    }

    public Portfolio[] GetPortfolios()
    {
        return _db.Portfolios.ToArray();
    }
}
