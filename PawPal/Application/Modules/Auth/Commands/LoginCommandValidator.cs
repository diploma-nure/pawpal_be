namespace Application.Modules.Auth.Commands;

public sealed class LoginCommandValidator
    : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty();

        RuleFor(command => command.Password)
            .NotEmpty();
    }
}
