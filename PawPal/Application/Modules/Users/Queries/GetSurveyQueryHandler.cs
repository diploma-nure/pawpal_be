namespace Application.Modules.Users.Queries;

public class GetSurveyQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetSurveyQuery, SurveyDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<SurveyDto> Handle(GetSurveyQuery query, CancellationToken cancellationToken)
    {
        Survey? survey = null;
        SurveyDto? result = null;

        if (!query.SurveyId.HasValue && !query.UserId.HasValue)
        {
            var userId = (_dbContext.User ?? throw new UnauthorizedException()).Id;
            survey = await _dbContext.Surveys
                .Include(s => s.OwnerDetails)
                .Include(s => s.ResidenceDetails)
                .Include(s => s.PetPreferences)
                    .ThenInclude(p => p.DesiredFeatures)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken)
                ?? throw new NotFoundException(Constants.ResponseCodes.NotFoundSurvey, $"User with id {query.SurveyId} does not have completed survey");

            result = survey.ToSurveyDto();
            return result;
        }

        if (query.SurveyId.HasValue)
        {
            survey = await _dbContext.Surveys
                .Include(s => s.OwnerDetails)
                .Include(s => s.ResidenceDetails)
                .Include(s => s.PetPreferences)
                    .ThenInclude(p => p.DesiredFeatures)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == query.SurveyId, cancellationToken)
                ?? throw new NotFoundException(Constants.ResponseCodes.NotFoundSurvey, $"Survey with id {query.SurveyId} not found");
        }
        else
        {
            survey = await _dbContext.Surveys
                .Include(s => s.OwnerDetails)
                .Include(s => s.ResidenceDetails)
                .Include(s => s.PetPreferences)
                    .ThenInclude(p => p.DesiredFeatures)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.UserId == query.UserId, cancellationToken)
                ?? throw new NotFoundException(Constants.ResponseCodes.NotFoundSurvey, $"Survey for user with id {query.UserId} not found");
        }

        if (_dbContext.User!.Role is not Role.Admin && survey.UserId != _dbContext.User!.Id)
            throw new ForbiddenException();

        result = survey.ToSurveyDto();
        return result;
    }
}
