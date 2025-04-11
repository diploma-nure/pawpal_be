namespace Application.Interfaces;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(int userId);

    Task<int?> ValidateTokenAsync(string? token);
}
