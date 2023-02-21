using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Template.Shared.Areas.Localization.Extensions;
using Template.Shared.Areas.Localization.Parameters;

namespace Template.Server.Areas.Localization.Controllers;

[ApiController]
[Route("api")]
public class CultureController : ControllerBase
{
    private readonly IConfiguration _config;

    public CultureController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("culture")]
    public IActionResult SetCulture(CultureFormParameters parameters)
    {
        Response.Cookies.Append(
            _config.GetLocalizationCookieName(),
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(parameters.CultureName)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

        return Ok();
    }
}
