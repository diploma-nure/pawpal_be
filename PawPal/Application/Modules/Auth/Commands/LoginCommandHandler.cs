namespace Application.Modules.Auth.Commands;

public class LoginCommandHandler(IApplicationDbContext dbContext, ITokenService tokenService)
    : IRequestHandler<LoginCommand, LoginResponseDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    private readonly ITokenService _tokenService = tokenService;

    public async Task<LoginResponseDto> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var normalizedEmail = command.Email.ToNormalizedEmail();
        var passwordHash = command.Password.ToSha256Hash();
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == normalizedEmail);

        string? token = null;
        if (user != null)
        {
            if (user.PasswordHash != passwordHash)
                throw new UnauthorizedException(Constants.ResponseCodes.AuthLoginFailed, "Login attempt failed");

            token = await _tokenService.GenerateTokenAsync(user.Id);
            return new LoginResponseDto() { Token = token, IsNewUser = false };
        }

        user = new User
        {
            Email = normalizedEmail,
            Role = Role.User,
            PasswordHash = passwordHash,
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        token = await _tokenService.GenerateTokenAsync(user.Id);

        return new LoginResponseDto() { Token = token, IsNewUser = true };
    }
}
