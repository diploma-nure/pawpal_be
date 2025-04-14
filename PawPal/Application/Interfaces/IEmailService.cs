namespace Application.Interfaces;

public interface IEmailService
{
    Task SendPasswordRecoveryEmailAsync(string email, string recoveryCode, CancellationToken cancellationToken);
}
