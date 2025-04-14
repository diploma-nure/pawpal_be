﻿namespace Application.Modules.Auth.Commands;

public class ValidatePasswordRecoveryCodeCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<ValidatePasswordRecoveryCodeCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(ValidatePasswordRecoveryCodeCommand command, CancellationToken cancellationToken)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == command.UserId)
            ?? throw new NotFoundException($"User with id {command.UserId} not found");

        if (user.PasswordRecoveryCode is null)
            throw new ConflictException($"User with id {command.UserId} does not have assigned recovery code");

        if (!user.PasswordRecoveryCode.Equals(command.RecoveryCode))
            throw new ConflictException("Invalid recovery code");

        return user.Id;
    }
}
