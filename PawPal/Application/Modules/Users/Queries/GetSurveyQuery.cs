namespace Application.Modules.Users.Queries;

public class GetSurveyQuery : IRequest<SurveyDto>
{
    public int? Id { get; set; }
}
