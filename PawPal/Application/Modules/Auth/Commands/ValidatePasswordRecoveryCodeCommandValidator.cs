namespace Application.Modules.Auth.Commands;

public sealed class ValidatePasswordRecoveryCodeCommandValidator
    : AbstractValidator<ValidatePasswordRecoveryCodeCommand>
{
    public ValidatePasswordRecoveryCodeCommandValidator()
    {
        RuleFor(command => command.UserId)
            .NotEmpty();

        RuleFor(command => command.RecoveryCode)
            .NotEmpty()
            .Length(6);
    }
}
