using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Template.Shared.Models;
using Template.Shared.Services;

namespace Template.Server.Services;

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
        var url = $"{_config["App:Url"]}/account/confirm-email/{user.Id}/"
            + WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        var body = _localizer["ConfirmEmailEmailBody"]
            .Replace("{user}", user.UserName)
            .Replace("{url}", url)
            .Replace("{app}", _config["App:Name"]);

        _emailSender.Send(
            _config["Smtp:Username"],
            user.Email,
            _localizer["ConfirmYourEmail"],
            body);
    }
}
