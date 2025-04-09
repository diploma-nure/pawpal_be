namespace Application.Modules.Pets.Commands;

public sealed class AddPetCommandValidator
    : AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty();

        RuleFor(command => command.Gender)
            .NotNull()
            .IsInEnum();

        RuleFor(command => command.Size)
            .NotNull()
            .IsInEnum();

        RuleFor(command => command.AgeMonths)
            .NotNull()
            .LessThanOrEqualTo(12);

        RuleFor(command => command.AgeYears)
            .NotNull()
            .LessThanOrEqualTo(30);

        RuleFor(command => new { command.AgeYears, command.AgeMonths })
            .Must(x => x.AgeYears > 0 || x.AgeMonths > 0);

        RuleFor(command => command.HasSpecialNeeds)
            .NotNull();
    }
}
