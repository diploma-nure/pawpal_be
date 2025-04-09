namespace Application.Modules.Admin.Commands;

public class RegisterAdminCommand : IRequest<int>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
