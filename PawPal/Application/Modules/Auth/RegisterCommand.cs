namespace Application.Modules.Auth;

public class RegisterCommand : IRequest<string>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
