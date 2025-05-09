namespace Application.Modules.Applications.Queries;

public sealed class GetApplicationsFilteredQueryValidator
    : AbstractValidator<GetApplicationsFilteredQuery>
{
    public GetApplicationsFilteredQueryValidator()
    {
        RuleFor(query => query.Statuses)
            .ForEach(status => status.IsInEnum())
            .When(query => query.Statuses is not null);

        RuleFor(query => query.Pagination)
            .NotNull()
            .SetValidator(new PaginationDtoValidator());
    }
}
