﻿namespace Application.Modules.Pets.Queries;

public class GetPetsFilteredQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetPetsFilteredQuery, PaginatedListDto<PetInListDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<PaginatedListDto<PetInListDto>> Handle(GetPetsFilteredQuery query, CancellationToken cancellationToken)
    {
        var pets = _dbContext.Pets
            .AsNoTracking()
            .Include(p => p.Features)
            .Include(p => p.Pictures)
            .FilterSoftDeleted();

        pets = ApplyFiltering(pets, query.Species);
        pets = ApplyFiltering(pets, query.Sizes);
        pets = ApplyFiltering(pets, query.Ages);
        pets = ApplyFiltering(pets, query.Genders);
        pets = ApplyFiltering(pets, query.HasSpecialNeeds);

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

    private static IQueryable<Pet> ApplyFiltering(IQueryable<Pet> pets, List<PetSpecies>? species)
        => species is null ? pets : pets.Where(p => species.Contains(p.Species));

    private static IQueryable<Pet> ApplyFiltering(IQueryable<Pet> pets, List<PetSize>? sizes)
        => sizes is null ? pets : pets.Where(p => sizes.Contains(p.Size));

    private static IQueryable<Pet> ApplyFiltering(IQueryable<Pet> pets, List<PetAge>? ages)
        => ages is null ? pets : pets.Where(p => ages.Contains(p.Age));

    private static IQueryable<Pet> ApplyFiltering(IQueryable<Pet> pets, List<PetGender>? genders)
        => genders is null ? pets : pets.Where(p => genders.Contains(p.Gender));

    private static IQueryable<Pet> ApplyFiltering(IQueryable<Pet> pets, bool? hasSpecialNeeds)
        => hasSpecialNeeds.HasValue ? pets.Where(p => p.HasSpecialNeeds == hasSpecialNeeds.Value) : pets;

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
                _ => pets.OrderBy(p => p.Name)
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
                _ => pets.OrderByDescending(p => p.Name)
            };
        }
        else
            pets = pets.OrderBy(p => p.Name);

        return pets;
    }
}
