namespace Application.Modules.Pets.Queries;

public class GetPetsFilteredQuery : IRequest<PaginatedListDto<PetInListDto>>
{
    public PaginationDto Pagination { get; set; }

    public SortingDto<PetSortingOptions> Sorting { get; set; }
}
