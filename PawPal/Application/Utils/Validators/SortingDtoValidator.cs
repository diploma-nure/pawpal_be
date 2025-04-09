namespace Application.Utils.Validators;

public sealed class SortingDtoValidator<TSortingType>
    : AbstractValidator<SortingDto<TSortingType>>
    where TSortingType : Enum
{
    public SortingDtoValidator()
    {
        RuleFor(dto => dto.Type)
            .NotNull()
            .Must(type => type == null || Enum.IsDefined(typeof(TSortingType), type));

        RuleFor(dto => dto.Direction)
            .NotNull()
            .IsInEnum();
    }
}
