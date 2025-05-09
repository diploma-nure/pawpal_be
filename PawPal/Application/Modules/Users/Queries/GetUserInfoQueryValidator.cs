namespace Application.Modules.Users.Queries;

public sealed class GetUserInfoQueryValidator
    : AbstractValidator<GetUserInfoQuery>
{
    public GetUserInfoQueryValidator()
    {
        RuleFor(query => query.UserId)
            .GreaterThan(0)
            .When(query => query.UserId.HasValue);
    }
}
