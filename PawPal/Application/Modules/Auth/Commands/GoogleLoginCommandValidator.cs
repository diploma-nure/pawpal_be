namespace Application.Modules.Auth.Commands;

public sealed class GoogleLoginCommandValidator
    : AbstractValidator<GoogleLoginCommand>
{
    public GoogleLoginCommandValidator()
    {
        RuleFor(command => command.Token)
            .NotEmpty();
    }
}
