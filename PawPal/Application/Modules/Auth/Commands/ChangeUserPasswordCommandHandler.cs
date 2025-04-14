namespace Application.Modules.Auth.Commands;

public class ChangeUserPasswordCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<ChangeUserPasswordCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(ChangeUserPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == command.UserId)
            ?? throw new NotFoundException($"User with id {command.UserId} not found");

        if (user.PasswordRecoveryCode is null)
            throw new ConflictException($"User with id {command.UserId} does not have assigned recovery code");

        if (!user.PasswordRecoveryCode.Equals(command.RecoveryCode))
            throw new ConflictException("Invalid recovery code");

        if (!command.NewPassword1.Equals(command.NewPassword2))
            throw new ConflictException("Passwords do not match");

        var passwordHash = command.NewPassword1.ToSha256Hash();
        user.PasswordHash = passwordHash;
        user.PasswordRecoveryCode = null;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
