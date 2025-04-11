namespace Application.Modules.Pets.Queries;

public sealed class GetPetsFilteredQueryValidator
    : AbstractValidator<GetPetsFilteredQuery>
{
    public GetPetsFilteredQueryValidator()
    {
        RuleFor(query => query.Species)
            .ForEach(species => species.IsInEnum())
            .When(query => query.Species is not null);

        RuleFor(query => query.Sizes)
            .ForEach(size => size.IsInEnum())
            .When(query => query.Sizes is not null);

        RuleFor(query => query.Ages)
            .ForEach(age => age.IsInEnum())
            .When(query => query.Ages is not null);

        RuleFor(query => query.Genders)
            .ForEach(gender => gender.IsInEnum())
            .When(query => query.Genders is not null);

        RuleFor(query => query.Pagination)
            .NotNull()
            .SetValidator(new PaginationDtoValidator());

        RuleFor(query => query.Sorting)
            .NotNull()
            .SetValidator(new SortingDtoValidator<PetSortingOptions>());
    }
}
