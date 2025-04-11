namespace Application.Modules.Pets.Queries;

public class GetPetsFilteredQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetPetsFilteredQuery, PaginatedListDto<PetInListDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<PaginatedListDto<PetInListDto>> Handle(GetPetsFilteredQuery query, CancellationToken cancellationToken)
    {
        var pets = _dbContext.Pets
            .Include(p => p.Features)
            .AsNoTracking();

        // todo: apply filtering here
        var count = pets.Count();
        pets = ApplySorting(pets, query.Sorting!);
        pets = pets.Paginate(query.Pagination.Page, query.Pagination.PageSize);

        var result = new PaginatedListDto<PetInListDto>()
        {
            Items = await pets
                .Select(p => p.ToPetInListDto())
                .ToListAsync(cancellationToken),
            Page = query.Pagination.Page!.Value,
            PageSize = query.Pagination.PageSize!.Value,
            Count = count,
        };

        return result;
    }

    private static IQueryable<Pet> ApplySorting(IQueryable<Pet> pets, SortingDto<PetSortingOptions> sortingDto)
    {
        if (sortingDto.Direction is SortingDirection.Asc)
        {
            pets = sortingDto.Type switch
            {
                PetSortingOptions.Name => pets.OrderBy(p => p.Name),
                PetSortingOptions.Age => pets.OrderBy(p => p.Age),
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
                PetSortingOptions.Age => pets.OrderByDescending(p => p.Age),
                PetSortingOptions.Recent => pets.OrderByDescending(p => p.CreatedAt),
                PetSortingOptions.Size => pets.OrderByDescending(p => p.Size),
                _ => pets
            };
        }

        return pets;
    }
}
