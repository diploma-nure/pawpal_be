namespace Application.Modules.Auth.Commands;

public class GoogleLoginCommand : IRequest<string>
{
    public string Token { get; set; }
}
