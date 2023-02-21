using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Template.Server.Services;
using Template.Shared.Areas.Identity.Models;
using Template.Shared.Areas.Localization.Services;
using Template.Shared.Extensions;

namespace Template.Server.Areas.Identity.Services;

public interface IUserEmailSender
{
    public void SendEmailConfirmationUrl(User user, string token);
}

public class UserEmailSender : IUserEmailSender
{
    private readonly IConfiguration _config;
    private readonly IEmailSender _emailSender;
    private readonly Localizer _localizer;

    public UserEmailSender(IConfiguration config, IEmailSender emailSender, Localizer localizer)
    {
        _config = config;
        _emailSender = emailSender;
        _localizer = localizer;
    }

    public void SendEmailConfirmationUrl(User user, string token)
    {
        var url = $"{_config.GetAppUrl()}/account/confirm-email/{user.Id}/"
            + WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        var body = _localizer["ConfirmEmailEmailBody"]
            .Replace("{user}", user.UserName)
            .Replace("{url}", url)
            .Replace("{app}", _config.GetAppName());

        _emailSender.Send(
            _config.GetSmtpUserName(),
            user.Email,
            _localizer["ConfirmYourEmail"],
            body);
    }
}
