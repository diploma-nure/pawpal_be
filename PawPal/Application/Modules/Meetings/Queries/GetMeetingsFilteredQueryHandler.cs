namespace Application.Modules.Meetings.Queries;

public class GetMeetingsFilteredQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetMeetingsFilteredQuery, PaginatedListDto<MeetingInListDto>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<PaginatedListDto<MeetingInListDto>> Handle(GetMeetingsFilteredQuery query, CancellationToken cancellationToken)
    {
        var meetings = _dbContext.Meetings
            .Include(m => m.Application)
                .ThenInclude(a => a.User)
            .Include(m => m.Application)
                .ThenInclude(a => a.Pet)
                    .ThenInclude(p => p.Pictures)
            .AsNoTracking()
            .OrderByDescending(a => a.Start)
            .AsQueryable();

        if (_dbContext.User?.Role is not Role.Admin)
            throw new ForbiddenException();

        meetings = ApplyFiltering(meetings, query.Statuses);

        var count = meetings.Count();
        meetings = meetings.Paginate(query.Pagination.Page, query.Pagination.PageSize);

        var result = new PaginatedListDto<MeetingInListDto>()
        {
            Items = await meetings
                .Select(a => a.ToMeetingInListDto())
                .ToListAsync(cancellationToken),
            Page = query.Pagination.Page!.Value,
            PageSize = query.Pagination.PageSize!.Value,
            Count = count,
        };

        return result;
    }

    private static IQueryable<Meeting> ApplyFiltering(IQueryable<Meeting> meetings, List<MeetingStatus>? statuses)
        => statuses is null ? meetings : meetings.Where(p => statuses.Contains(p.Status));
}
