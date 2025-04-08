namespace Application.Modules.Auth;

public class RegisterCommandHandler(IApplicationDbContext dbContext, ITokenService tokenService)
    : IRequestHandler<RegisterCommand, string>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    private readonly ITokenService _tokenService = tokenService;

    public async Task<string> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (_dbContext.Users.Any(u => u.Email == command.Email))
            throw new ConflictException($"User with email {command.Email} already exists");

        var passwordHash = command.Password.ToSha256Hash();
        var user = new User
        {
            Email = command.Email,
            PasswordHash = passwordHash,
            Role = Role.User,
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveShangesAsync(cancellationToken);

        var token = await _tokenService.GenerateToken(user.Id);

        return token;
    }
}
