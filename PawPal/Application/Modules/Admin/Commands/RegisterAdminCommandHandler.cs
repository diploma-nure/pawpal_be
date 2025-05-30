namespace Application.Modules.Admin.Commands;

public class RegisterAdminCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<RegisterAdminCommand, int>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<int> Handle(RegisterAdminCommand command, CancellationToken cancellationToken)
    {
        if (_dbContext.User?.Role is not Role.Admin)
            throw new ForbiddenException();

        if (_dbContext.Users.Any(u => u.Email == command.Email))
            throw new ConflictException(Constants.ResponseCodes.ConflictUserExists, $"User with email {command.Email} already exists");

        var passwordHash = command.Password.ToSha256Hash();
        var user = new User
        {
            Email = command.Email,
            PasswordHash = passwordHash,
            Role = Role.Admin,
            FullName = command.FullName,
            PhoneNumber = command.PhoneNumber,
            Address = command.Address,
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
