namespace Application.Modules.Users.Commands;

public class UpdateUserInfoCommand : IRequest<int>
{
    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }
}
