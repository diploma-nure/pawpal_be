﻿namespace Application.Modules.Auth.Commands;

public class RegisterCommand : IRequest<string>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
