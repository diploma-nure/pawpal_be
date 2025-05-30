﻿namespace Application.Modules.Users.Commands;

public sealed class UpdateUserInfoCommandValidator
    : AbstractValidator<UpdateUserInfoCommand>
{
    public UpdateUserInfoCommandValidator()
    {
        RuleFor(command => command.FullName)
            .MaximumLength(100)
            .When(command => command.FullName is not null);

        RuleFor(command => command.PhoneNumber)
            .Matches(Constants.Patterns.PhoneNumber)
            .When(command => command.PhoneNumber is not null);

        RuleFor(command => command.Address)
            .MaximumLength(200)
            .When(command => command.Address is not null);
    }
}
