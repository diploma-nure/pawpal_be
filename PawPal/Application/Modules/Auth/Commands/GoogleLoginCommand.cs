namespace Application.Modules.Auth.Commands;

public class GoogleLoginCommand : IRequest<LoginResponseDto>
{
    public string Token { get; set; }
}
