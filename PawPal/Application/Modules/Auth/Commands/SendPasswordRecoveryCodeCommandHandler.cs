namespace Application.Modules.Auth.Commands;

public class SendPasswordRecoveryCodeCommandHandler(IApplicationDbContext dbContext, IEmailService emailService)
    : IRequestHandler<SendPasswordRecoveryCodeCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    private readonly IEmailService _emailService = emailService;

    public async Task<int> Handle(SendPasswordRecoveryCodeCommand command, CancellationToken cancellationToken)
    {
        var normalizedEmail = command.Email.ToNormalizedEmail();

        var user = _dbContext.Users.FirstOrDefault(u => u.Email == normalizedEmail)
            ?? throw new NotFoundException(Constants.ResponseCodes.NotFoundUser, $"User with email {normalizedEmail} not found");

        var recoveryCode = SecurityHelper.GenerateRecoveryCode();
        user.PasswordRecoveryCode = recoveryCode;
        await _dbContext.SaveChangesAsync(cancellationToken);

        await _emailService.SendPasswordRecoveryEmailAsync(user.Email, recoveryCode, cancellationToken);

        return user.Id;
    }
}
