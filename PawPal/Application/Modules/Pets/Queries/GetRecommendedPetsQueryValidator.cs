namespace Application.Modules.Pets.Queries;

public sealed class GetRecommendedPetsQueryValidator
    : AbstractValidator<GetRecommendedPetsQuery>
{
    public GetRecommendedPetsQueryValidator()
    {
        RuleFor(query => query.Pagination)
            .NotNull()
            .SetValidator(new PaginationDtoValidator());
    }
}
