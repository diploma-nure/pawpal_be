namespace Application.Modules.Pets.Queries;

public sealed class GetPetsFilteredQueryValidator
    : AbstractValidator<GetPetsFilteredQuery>
{
    public GetPetsFilteredQueryValidator()
    {
        RuleFor(query => query.Pagination)
            .NotNull()
            .SetValidator(new PaginationDtoValidator());

        RuleFor(query => query.Sorting)
            .NotNull()
            .SetValidator(new SortingDtoValidator<PetSortingOptions>());
    }
}
