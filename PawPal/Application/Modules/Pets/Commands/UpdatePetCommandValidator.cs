namespace Application.Modules.Pets.Commands;

public sealed class UpdatePetCommandValidator
    : AbstractValidator<AddPetCommand>
{
    public UpdatePetCommandValidator()
    {
        RuleFor(command => command.Species)
            .IsInEnum()
            .When(command => command.Species is not null);

        RuleFor(command => command.Gender)
            .IsInEnum()
            .When(command => command.Gender is not null);

        RuleFor(command => command.Size)
            .IsInEnum()
            .When(command => command.Size is not null);

        RuleFor(command => command.Age)
            .IsInEnum()
            .When(command => command.Age is not null);
    }
}
