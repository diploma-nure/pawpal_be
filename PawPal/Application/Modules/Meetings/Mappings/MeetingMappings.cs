namespace Application.Modules.Meetings.Mappings;

public static class MeetingMappings
{
    public static MeetingInListDto ToMeetingInListDto(this Meeting meeting)
        => new()
        {
            Id = meeting.Id,
            Status = meeting.Status,
            Start = meeting.Start,
            User = meeting.Application.User.ToUserShortDto(),
            Pet = meeting.Application.Pet.ToPetShortDto(),
            Application = meeting.Application.ToApplicationShortDto()
        };
}