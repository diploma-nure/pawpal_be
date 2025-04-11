namespace Application.Modules.Pets.Queries;

public class GetPetsFilteredQuery : IRequest<PaginatedListDto<PetInListDto>>
{
    public List<PetSpecies>? Species { get; set; }

    public List<PetSize>? Sizes { get; set; }

    public List<PetAge>? Ages { get; set; }

    public List<PetGender>? Genders { get; set; }

    public PaginationDto Pagination { get; set; }

    public SortingDto<PetSortingOptions> Sorting { get; set; }
}
