namespace Application.Modules.Users.Queries;

public class GetSurveyQuery : IRequest<SurveyDto>
{
    public int? SurveyId { get; set; }

    public int? UserId { get; set; }
}
