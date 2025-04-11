namespace Application.Modules.Auth.Dtos;

public class LoginResponseDto
{
    public string Token { get; set; }

    public bool IsNewUser { get; set; }
}
