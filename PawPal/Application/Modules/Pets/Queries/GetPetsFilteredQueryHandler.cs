namespace Application.Modules.Pets.Queries;

public class GetPetsFilteredQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetPetsFilteredQuery, List<PetInListDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<List<PetInListDto>> Handle(GetPetsFilteredQuery command, CancellationToken cancellationToken)
    {
        var pets = _dbContext.Pets.AsNoTracking();

        pets = ApplySorting(pets, command.Sorting!);

        pets = pets.Paginate(command.Pagination.Page.Value, command.Pagination.PageSize.Value);

        var result = await pets
            .Select(p => p.ToPetInListDto())
            .ToListAsync(cancellationToken);

        return result;
    }

    private static IQueryable<Pet> ApplySorting(IQueryable<Pet> pets, SortingDto<PetSortingOptions> sortingDto)
    {
        if (sortingDto.Direction is SortingDirection.Asc)
        {
            pets = sortingDto.Type switch
            {
                PetSortingOptions.Name => pets.OrderBy(p => p.Name),
                PetSortingOptions.Age => pets.OrderBy(p => p.AgeMonths),
                PetSortingOptions.Recent => pets.OrderBy(p => p.CreatedAt),
                PetSortingOptions.Size => pets.OrderBy(p => p.Size),
                _ => pets
            };
        }
        else if (sortingDto.Direction is SortingDirection.Desc)
        {
            pets = sortingDto.Type switch
            {
                PetSortingOptions.Name => pets.OrderByDescending(p => p.Name),
                PetSortingOptions.Age => pets.OrderByDescending(p => p.AgeMonths),
                PetSortingOptions.Recent => pets.OrderByDescending(p => p.CreatedAt),
                PetSortingOptions.Size => pets.OrderByDescending(p => p.Size),
                _ => pets
            };
        }

        return pets;
    }
}
