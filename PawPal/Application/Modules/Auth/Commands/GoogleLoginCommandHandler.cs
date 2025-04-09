namespace Application.Modules.Auth.Commands;

public class GoogleLoginCommandHandler(IApplicationDbContext dbContext, ITokenService tokenService)
    : IRequestHandler<GoogleLoginCommand, string>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    private readonly ITokenService _tokenService = tokenService;

    public async Task<string> Handle(GoogleLoginCommand command, CancellationToken cancellationToken)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(command.Token);
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == payload.Email);

        string? token = null;
        if (user != null)
        {
            token = await _tokenService.GenerateToken(user.Id);
            return token;
        }

        user = new User
        {
            Email = payload.Email,
            Role = Role.User,
            ProfilePictureUrl = payload.Picture,
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveShangesAsync(cancellationToken);

        token = await _tokenService.GenerateToken(user.Id);

        return token;
    }
}
