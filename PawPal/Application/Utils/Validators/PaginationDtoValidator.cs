namespace Application.Utils.Validators;

public sealed class PaginationDtoValidator
    : AbstractValidator<PaginationDto>
{
    public PaginationDtoValidator()
    {
        RuleFor(dto => dto.Page)
            .NotNull()
            .GreaterThan(0);

        RuleFor(dto => dto.PageSize)
            .NotNull()
            .GreaterThan(0);
    }
}
