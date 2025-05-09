namespace Application.Modules.Applications.Queries;

public class GetApplicationsFilteredQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetApplicationsFilteredQuery, PaginatedListDto<ApplicationInListDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<PaginatedListDto<ApplicationInListDto>> Handle(GetApplicationsFilteredQuery query, CancellationToken cancellationToken)
    {
        var applications = _dbContext.Applications
            .Include(a => a.User)
            .Include(a => a.Pet)
                .ThenInclude(p => p.Pictures)
            .AsNoTracking()
            .OrderByDescending(a => a.CreatedAt)
            .AsQueryable();

        if (_dbContext.User?.Role is Role.User)
            applications = applications.Where(a => a.UserId == _dbContext.User!.Id);

        applications = ApplyFiltering(applications, query.Statuses);

        var count = applications.Count();
        applications = applications.Paginate(query.Pagination.Page, query.Pagination.PageSize);

        var result = new PaginatedListDto<ApplicationInListDto>()
        {
            Items = await applications
                .Select(a => a.ToApplicationInListDto())
                .ToListAsync(cancellationToken),
            Page = query.Pagination.Page!.Value,
            PageSize = query.Pagination.PageSize!.Value,
            Count = count,
        };

        return result;
    }

    private static IQueryable<PetApplication> ApplyFiltering(IQueryable<PetApplication> applications, List<ApplicationStatus>? statuses)
        => statuses is null ? applications : applications.Where(p => statuses.Contains(p.Status));
}
