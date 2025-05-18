namespace Application.Modules.Auth.Commands;

public sealed class LoginCommandValidator
    : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .Matches(Constants.Patterns.Email);

        RuleFor(command => command.Password)
            .NotEmpty();
    }
}
