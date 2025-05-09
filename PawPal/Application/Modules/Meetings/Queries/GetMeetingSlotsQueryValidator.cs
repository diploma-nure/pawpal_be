namespace Application.Modules.Meetings.Queries;

public sealed class GetMeetingSlotsQueryValidator
    : AbstractValidator<GetMeetingSlotsQuery>
{
    public GetMeetingSlotsQueryValidator()
    {
        RuleFor(query => query.StartDate)
            .NotEmpty();

        RuleFor(query => query.EndDate)
            .NotEmpty();
    }
}
