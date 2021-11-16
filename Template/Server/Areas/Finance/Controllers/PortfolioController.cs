using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Template.Server.Areas.Finance.Services;
using Template.Server.Areas.Identity.Services;
using Template.Shared.Areas.Finance.Models;

namespace Template.Server.Areas.Finance.Controllers;

[ApiController]
[Route("api")]
public class PortfolioController : ControllerBase
{
    private readonly IPortfolioService _portfolioService;
    private readonly IUserService _userService;

    public PortfolioController(IPortfolioService portfolioService, IUserService userService)
    {
        _portfolioService = portfolioService;
        _userService = userService;
    }

    [Authorize]
    [HttpGet("portfolios")]
    public async Task<Portfolio[]> GetPortfolios(string user)
    {
        return _portfolioService.GetPortfolios(await _userService.GetUserById(user));
    }
}
