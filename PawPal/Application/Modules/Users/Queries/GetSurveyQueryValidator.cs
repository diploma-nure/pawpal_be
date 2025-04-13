namespace Application.Modules.Users.Queries;

public sealed class GetSurveyQueryValidator
    : AbstractValidator<GetSurveyQuery>
{
    public GetSurveyQueryValidator()
    {
        RuleFor(query => query.Id)
            .GreaterThan(0)
            .When(query => query.Id.HasValue);
    }
}
