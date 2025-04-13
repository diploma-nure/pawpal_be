namespace Application.Modules.Users.Queries;

public class GetSurveyQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetSurveyQuery, SurveyDto>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<SurveyDto> Handle(GetSurveyQuery query, CancellationToken cancellationToken)
    {
        Survey? survey = null;
        SurveyDto? result = null;

        if (query.Id is null)
        {
            var userId = (_dbContext.User ?? throw new UnauthorizedException()).Id;
            survey = await _dbContext.Surveys
                .Include(s => s.OwnerDetails)
                .Include(s => s.ResidenceDetails)
                .Include(s => s.PetPreferences)
                    .ThenInclude(p => p.DesiredFeatures)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken)
                ?? throw new NotFoundException($"User with id {query.Id} does not have completed survey");

            result = survey.ToSurveyDto();
            return result;
        }

        survey = await _dbContext.Surveys
            .Include(s => s.OwnerDetails)
            .Include(s => s.ResidenceDetails)
            .Include(s => s.PetPreferences)
                .ThenInclude(p => p.DesiredFeatures)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == query.Id, cancellationToken)
            ?? throw new NotFoundException($"Survey with id {query.Id} not found");

        if (_dbContext.User!.Role is not Role.Admin && survey.UserId != _dbContext.User!.Id)
            throw new ForbiddenException();

        result = survey.ToSurveyDto();
        return result;
    }
}
