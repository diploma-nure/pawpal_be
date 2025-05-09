namespace Application.Modules.Pets.Queries;

public class GetRecommendedPetsQuery : IRequest<PaginatedListDto<PetInListDto>>
{
    public PaginationDto Pagination { get; set; }
}
