using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Template.Server.Areas.Identity.Services;
using Template.Shared.Areas.Identity.Models;
using Template.Shared.Areas.Identity.Parameters;
using Template.Shared.Areas.Localization.Services;

namespace Template.Server.Areas.Identity.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly Localizer _localizer;
    private readonly IUserEmailSender _userEmailSender;
    private readonly IUserService _userService;

    public UserController(Localizer localizer, IUserEmailSender userEmailSender, IUserService userService)
    {
        _localizer = localizer;
        _userEmailSender = userEmailSender;
        _userService = userService;
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(AccountConfirmEmailParameters parameters)
    {
        var user = await _userService.GetUserById(parameters.Id);

        if (user == null)
        {
            return BadRequest(_localizer["ThisUserDoesNotExist"]);
        }

        var result = await _userService.ConfirmEmail(
            user,
            Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(parameters.Token)));

        if (!result.HasSucceeded)
        {
            return BadRequest(result.Messages[0]);
        }

        await _userService.LogIn(user);
        return Ok();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(AccountCreateParameters parameters)
    {
        var user = new User
        {
            UserName = parameters.UserName,
            Email = parameters.Email
        };

        var result = await _userService.Create(user, parameters.Password);

        if (!result.HasSucceeded)
        {
            return BadRequest(result.Messages[0]);
        }

        var token = await _userService.GenerateEmailConfirmationToken(user);
        _userEmailSender.SendEmailConfirmationUrl(user, token);
        await _userService.LogIn(user);
        return Ok();
    }

    [HttpGet("info")]
    public async Task<UserInfo> GetUserInfo()
    {
        var info = new UserInfo
        {
            ExposedClaims = User.Claims.ToDictionary(c => c.Type, c => c.Value),
            IsAuthenticated = User.Identity.IsAuthenticated
        };

        if (info.IsAuthenticated)
        {
            var userName = User.Identity.Name;
            var user = await _userService.GetUserByUserName(userName);

            if (user != null)
            {
                info.CurrentUser = user;
            }
            else
            {
                await _userService.LogOut();
            }
        }

        return info;
    }

    [HttpPost("log-in")]
    public async Task<IActionResult> LogIn(AccountLogInParameters parameters)
    {
        var user = await _userService.GetUserByEmail(parameters.Email);

        if (user == null)
        {
            // Mask that a user with that email does not exist
            return BadRequest(_localizer["WrongPassword"]);
        }

        var result = await _userService.LogIn(user, parameters.Password);

        if (!result.HasSucceeded)
        {
            return BadRequest(_localizer["WrongPassword"]);
        }

        return Ok();
    }

    [Authorize]
    [HttpDelete("log-out")]
    public async Task<IActionResult> LogOut()
    {
        await _userService.LogOut();
        return Ok();
    }

    [Authorize]
    [HttpPut("resend-email-confirmation-url")]
    public async Task<IActionResult> ResendEmailConfirmationUrl(ResendEmailConfirmationUrlParameters parameters)
    {
        var user = await _userService.GetUserById(parameters.UserId);

        if (user == null)
        {
            return BadRequest(_localizer["ThisUserDoesNotExist"]);
        }

        var token = await _userService.GenerateEmailConfirmationToken(user);
        _userEmailSender.SendEmailConfirmationUrl(user, token);
        return Ok();
    }

    [Authorize]
    [HttpPut("password")]
    public async Task<IActionResult> SetPassword(SettingsPasswordParameters parameters)
    {
        var user = await _userService.GetCurrentUser(User);

        if (user == null)
        {
            return BadRequest(_localizer["ThisUserDoesNotExist"]);
        }

        var result = await _userService.SetPassword(user, parameters.CurrentPassword, parameters.NewPassword);

        if (!result.HasSucceeded)
        {
            return BadRequest(result.Message);
        }

        return Ok();
    }
}
