namespace Application.Modules.Users.Queries;

public sealed class GetUserInfoQueryValidator
    : AbstractValidator<GetUserInfoQuery>
{
    public GetUserInfoQueryValidator()
    {
        RuleFor(query => query.Id)
            .GreaterThan(0)
            .When(query => query.Id.HasValue);
    }
}
