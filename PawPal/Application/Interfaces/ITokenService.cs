namespace Application.Interfaces;

public interface ITokenService
{
    Task<string> GenerateToken(int userId);

    Task<int?> ValidateToken(string? token);
}
