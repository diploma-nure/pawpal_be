namespace Application.Modules.Auth.Commands;

public class GoogleLoginCommandHandler(IApplicationDbContext dbContext, ITokenService tokenService)
    : IRequestHandler<GoogleLoginCommand, LoginResponseDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    private readonly ITokenService _tokenService = tokenService;

    public async Task<LoginResponseDto> Handle(GoogleLoginCommand command, CancellationToken cancellationToken)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(command.Token);
        var normalizedEmail = payload.Email.ToNormalizedEmail();
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == normalizedEmail);

        string? token = null;
        if (user != null)
        {
            token = await _tokenService.GenerateTokenAsync(user.Id);
            return new LoginResponseDto() { Token = token, IsNewUser = false }; 
        }

        user = new User
        {
            Email = normalizedEmail,
            Role = Role.User,
            ProfilePictureUrl = payload.Picture,
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveShangesAsync(cancellationToken);

        token = await _tokenService.GenerateTokenAsync(user.Id);

        return new LoginResponseDto() { Token = token, IsNewUser = true };
    }
}
