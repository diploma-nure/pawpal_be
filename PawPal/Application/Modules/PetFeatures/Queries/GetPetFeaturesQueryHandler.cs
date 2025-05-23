﻿namespace Application.Modules.PetFeatures.Queries;

public class GetPetFeaturesQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetPetFeaturesQuery, List<PetFeatureInListDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<List<PetFeatureInListDto>> Handle(GetPetFeaturesQuery query, CancellationToken cancellationToken)
    {
        var features = await _dbContext.PetFeatures
            .AsNoTracking()
            .FilterSoftDeleted()
            .OrderBy(f => f.Feature)
            .ToListAsync(cancellationToken);

        var result = features
            .Select(f => f.ToPetInListDto())
            .ToList();

        return result;
    }
}
