namespace Application.Modules.Applications.Queries;

public class GetApplicationsFilteredQuery : IRequest<PaginatedListDto<ApplicationInListDto>>
{
    public List<ApplicationStatus>? Statuses { get; set; }

    public PaginationDto Pagination { get; set; }
}
