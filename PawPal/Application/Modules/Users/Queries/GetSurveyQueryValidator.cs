namespace Application.Modules.Users.Queries;

public sealed class GetSurveyQueryValidator
    : AbstractValidator<GetSurveyQuery>
{
    public GetSurveyQueryValidator()
    {
        RuleFor(query => query.SurveyId)
            .GreaterThan(0)
            .When(query => query.SurveyId.HasValue);

        RuleFor(query => query.UserId)
            .GreaterThan(0)
            .When(query => query.UserId.HasValue);
    }
}
