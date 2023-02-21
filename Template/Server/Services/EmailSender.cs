using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Template.Shared.Extensions;

namespace Template.Server.Services;

public interface IEmailSender
{
    public void Send(string from, string to, string subject, string body);
}

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration config)
    {
        _config = config;
    }

    public void Send(string from, string to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(from));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        message.Body = new TextPart(TextFormat.Html) { Text = body };

        using var smtp = new SmtpClient();
        smtp.Connect(_config.GetSmtpHost(), _config.GetSmtpPort(), SecureSocketOptions.StartTls);
        smtp.Authenticate(_config.GetSmtpUserName(), _config.GetSmtpPassword());
        smtp.Send(message);
        smtp.Disconnect(true);
    }
}
