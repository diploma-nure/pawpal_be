﻿namespace Application.Modules.Users.Dtos;

public class UserInfoDto
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }
}
