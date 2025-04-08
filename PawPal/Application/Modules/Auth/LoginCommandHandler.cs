namespace Application.Modules.Auth;

public class LoginCommandHandler(IApplicationDbContext dbContext, ITokenService tokenService)
    : IRequestHandler<LoginCommand, string>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    private readonly ITokenService _tokenService = tokenService;

    public async Task<string> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var passwordHash = command.Password.ToSha256Hash();
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == command.Email && u.PasswordHash == passwordHash)
            ?? throw new UnauthorizedException("Login attempt failed");
        
        var token = await _tokenService.GenerateToken(user.Id);
        return token;
    }
}
