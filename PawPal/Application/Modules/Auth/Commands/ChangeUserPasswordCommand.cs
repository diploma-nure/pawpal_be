namespace Application.Modules.Auth.Commands;

public class ChangeUserPasswordCommand : IRequest<int>
{
    public int UserId { get; set; }

    public string NewPassword1 { get; set; }

    public string NewPassword2 { get; set; }

    public string RecoveryCode { get; set; }
}
