namespace Application.Modules.Meetings.Queries;

public class GetMeetingsFilteredQuery : IRequest<PaginatedListDto<MeetingInListDto>>
{
    public List<MeetingStatus>? Statuses { get; set; }

    public PaginationDto Pagination { get; set; }
}
