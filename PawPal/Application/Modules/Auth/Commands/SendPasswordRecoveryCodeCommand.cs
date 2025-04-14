namespace Application.Modules.Auth.Commands;

public class SendPasswordRecoveryCodeCommand : IRequest<int>
{
    public string Email { get; set; }
}
