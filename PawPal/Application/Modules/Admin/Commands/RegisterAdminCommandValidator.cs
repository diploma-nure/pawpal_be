namespace Application.Modules.Admin.Commands;

public sealed class RegisterAdminCommandValidator
    : AbstractValidator<RegisterAdminCommand>
{
    public RegisterAdminCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .Matches(Constants.Patterns.Email);

        RuleFor(command => command.Password)
            .NotEmpty();

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
