namespace Application.Modules.Auth.Commands;

public class ValidatePasswordRecoveryCodeCommand : IRequest<int>
{
    public int UserId { get; set; }

    public string RecoveryCode { get; set; }
}
