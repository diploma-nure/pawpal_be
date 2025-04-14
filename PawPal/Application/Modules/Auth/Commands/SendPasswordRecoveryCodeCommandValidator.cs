namespace Application.Modules.Auth.Commands;

public sealed class SendPasswordRecoveryCodeCommandValidator
    : AbstractValidator<SendPasswordRecoveryCodeCommand>
{
    public SendPasswordRecoveryCodeCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty();
    }
}
