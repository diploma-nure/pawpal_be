namespace Infrastructure.Services;

public class EmailService(IOptions<EmailConfig> emailConfigOptions) : IEmailService
{
    private readonly EmailConfig _emailConfig = emailConfigOptions.Value;

    public async Task SendPasswordRecoveryEmailAsync(string email, string recoveryCode, CancellationToken cancellationToken)
    {
        var htmlTemplate = await File.ReadAllTextAsync(_emailConfig.Templates.PasswordRecovery.FilePath, cancellationToken);
        htmlTemplate = htmlTemplate.Replace("{{code}}", recoveryCode);

        await SendEmailAsync(email, htmlTemplate, cancellationToken);
    }

    private async Task SendEmailAsync(string email, string htmlBody, CancellationToken cancellationToken)
    {
        var emailBodyBuilder = new BodyBuilder
        {
            HtmlBody = htmlBody
        };

        var emailMessage = new MimeMessage();

        var from = new MailboxAddress(_emailConfig.Name, _emailConfig.EmailId);
        emailMessage.From.Add(from);
        var to = new MailboxAddress("", email);
        emailMessage.To.Add(to);

        emailMessage.Subject = _emailConfig.Templates.PasswordRecovery.Subject;
        emailMessage.Body = emailBodyBuilder.ToMessageBody();

        using var emailClient = new SmtpClient();
        emailClient.Connect(_emailConfig.Host, _emailConfig.Port, _emailConfig.UseSSL, cancellationToken);
        emailClient.Authenticate(_emailConfig.EmailId, _emailConfig.Password, cancellationToken);

        await emailClient.SendAsync(emailMessage, cancellationToken);

        emailClient.Disconnect(true, cancellationToken);
    }
}
