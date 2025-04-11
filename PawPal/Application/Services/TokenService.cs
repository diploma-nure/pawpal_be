namespace Application.Services;

public class TokenService(IApplicationDbContext dbContext, IOptions<AuthConfig> authConfigOptions) : ITokenService
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly AuthConfig _authConfig = authConfigOptions.Value;

    public async Task<string> GenerateTokenAsync(int userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new Exception($"User with id {userId} was not found");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_authConfig.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(Constants.Claims.IdClaim, user.Id.ToString()),
                new Claim(Constants.Claims.EmailClaim, user.Email),
                new Claim(Constants.Claims.RoleClaim, user.Role.ToString())
            ]),
            Expires = DateTime.UtcNow.AddHours(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<int?> ValidateTokenAsync(string? token)
    {
        if (token == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_authConfig.Secret);

        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
            };

            tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == Constants.Claims.IdClaim).Value);
            return userId;
        }
        catch
        {
            return null;
        }
    }
}
