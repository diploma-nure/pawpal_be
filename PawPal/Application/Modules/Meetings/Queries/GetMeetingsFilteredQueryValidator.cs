namespace Application.Modules.Meetings.Queries;

public sealed class GetMeetingsFilteredQueryValidator
    : AbstractValidator<GetMeetingsFilteredQuery>
{
    public GetMeetingsFilteredQueryValidator()
    {
        RuleFor(query => query.Statuses)
            .ForEach(status => status.IsInEnum())
            .When(query => query.Statuses is not null);

        RuleFor(query => query.Pagination)
            .NotNull()
            .SetValidator(new PaginationDtoValidator());
    }
}
