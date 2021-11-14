using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Server.Areas.Finance.Services;
using Template.Shared.Areas.Finance.Models;

namespace Template.Server.Areas.Finance.Controllers;

[ApiController]
[Route("api")]
public class PortfolioController : ControllerBase
{
    private readonly IPortfolioService _portfolioService;

    public PortfolioController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    [Authorize]
    [HttpGet("portfolios")]
    public Portfolio[] GetPortfolios()
    {
        return _portfolioService.GetPortfolios();
    }
}
