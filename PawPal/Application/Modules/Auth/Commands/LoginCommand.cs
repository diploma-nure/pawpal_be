namespace Application.Modules.Auth.Commands;

public class LoginCommand : IRequest<LoginResponseDto>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
