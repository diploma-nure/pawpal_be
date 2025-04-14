namespace Application.Modules.Auth.Commands;

public sealed class ChangeUserPasswordCommandValidator
    : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(command => command.UserId)
            .NotEmpty();

        RuleFor(command => command.NewPassword1)
            .NotEmpty();

        RuleFor(command => command.NewPassword2)
            .NotEmpty();

        RuleFor(command => command.RecoveryCode)
            .NotEmpty()
            .Length(6);
    }
}
