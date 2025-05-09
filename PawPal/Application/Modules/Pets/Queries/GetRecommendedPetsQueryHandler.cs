namespace Application.Modules.Pets.Queries;

public class GetRecommendedPetsQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetRecommendedPetsQuery, PaginatedListDto<PetInListDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<PaginatedListDto<PetInListDto>> Handle(GetRecommendedPetsQuery query, CancellationToken cancellationToken)
    {
        var userId = _dbContext.User!.Id;
        
        var survey = await _dbContext.Surveys
            .AsNoTracking()
            .Include(s => s.OwnerDetails)
            .Include(s => s.PetPreferences)
                .ThenInclude(s => s.DesiredFeatures)
            .Include(s => s.ResidenceDetails)
            .FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken)
            ?? throw new NotFoundException($"Survey not completed for user with id {userId} not found");

        var petRecommendationsQuery = _dbContext.SqlQueryRaw<PetRecommendationDto>(
                "SELECT * FROM get_pet_recommendations(@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13)",
                survey.OwnerDetails.HasOwnnedPetsBefore,
                survey.OwnerDetails.UnderstandsResponsibility,
                survey.OwnerDetails.HasSufficientFinancialResources,
                (int)survey.PetPreferences.DesiredActivityLevel,
                survey.PetPreferences.ReadyForSpecialNeedsPet,
                (int)survey.ResidenceDetails.PlaceOfResidence,
                survey.ResidenceDetails.HasSafeWalkingArea,
                (int)survey.ResidenceDetails.PetsAllowedAtResidence,
                survey.ResidenceDetails.HasOtherPets,
                survey.ResidenceDetails.HasSmallChildren,
                survey.PetPreferences.PreferredSpecies?.Select(s => (int)s).ToArray()!,
                survey.PetPreferences.PreferredSizes?.Select(s => (int)s).ToArray()!,
                survey.PetPreferences.PreferredAges?.Select(a => (int)a).ToArray()!,
                survey.PetPreferences.PreferredGenders?.Select(g => (int)g).ToArray()!,
                survey.PetPreferences.DesiredFeatures?.Select(f => (int)f.Id).ToArray()!
            );

        var count = petRecommendationsQuery.Count();
        petRecommendationsQuery = petRecommendationsQuery.Paginate(query.Pagination.Page, query.Pagination.PageSize);

        var petRecommendations = await petRecommendationsQuery.ToListAsync(cancellationToken);

        var pets = await _dbContext.Pets
            .Include(p => p.Features)
            .Include(p => p.Pictures)
            .AsNoTracking()
            .Where(p => petRecommendations.Select(p => p.PetId).Contains(p.Id))
            .ToListAsync(cancellationToken);

        var result = new PaginatedListDto<PetInListDto>()
        {
            Items = pets
                .Select(p => p.ToPetInListDto(petRecommendations.First(pm => pm.PetId == p.Id).MatchPercentage))
                .ToList(),
            Page = query.Pagination.Page!.Value,
            PageSize = query.Pagination.PageSize!.Value,
            Count = count,
        };

        return result;
    }
}