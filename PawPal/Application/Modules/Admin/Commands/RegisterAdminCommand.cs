﻿namespace Application.Modules.Admin.Commands;

public class RegisterAdminCommand : IRequest<int>
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }
}
