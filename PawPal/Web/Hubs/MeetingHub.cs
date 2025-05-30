namespace Web.Hubs;

[Auth]
public class MeetingHub(IApplicationDbContext context, ITokenService tokenService) : Hub
{
    private readonly IApplicationDbContext _dbContext = context;

    private readonly ITokenService _tokenService = tokenService;

    [HubMethodName("JoinMeeting")]
    public async Task JoinMeetingAsync(int meetingId, string token)
    {
        try
        {
            var userId = await _tokenService.ValidateTokenAsync(token);
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                await Clients.Caller.SendAsync("Error", $"User with id {userId} not found");
                return;
            }

            Context.Items["user_id"] = userId;

            var meeting = await _dbContext.Meetings
                .AsNoTracking()
                .Include(m => m.Application)
                .FirstOrDefaultAsync(m => m.Id == meetingId);

            if (meeting == null)
            {
                await Clients.Caller.SendAsync("Error", $"Meeting with id {meetingId} not found");
                return;
            }

            if (meeting.AdminId != userId && meeting.Application.UserId != userId)
            {
                await Clients.Caller.SendAsync("Error", $"Action forbidden");
                return;
            }

            var meetingGroup = GetMeetingGroupName(meeting.Id);
            await Groups.AddToGroupAsync(Context.ConnectionId, meetingGroup);

            await Clients.OthersInGroup(meetingGroup).SendAsync("UserJoined", user.Id, GetUserDisplayName(user));
        }
        catch
        {
            await Clients.Caller.SendAsync("Error", Constants.ResponseCodes.ConflictHubJoinFailed, "Failed to join meeting");
        }
    }

    [HubMethodName("SendWebRTCOffer")]
    public async Task SendWebRTCOfferAsync(int meetingId, object offer)
    {
        try
        {
            var userId = await GetUserIdAsync();
            if (userId == 0)
                return;

            await Clients.Group(GetMeetingGroupName(meetingId)).SendAsync("ReceiveWebRTCOffer", userId, offer);
        }
        catch
        {
            await Clients.Caller.SendAsync("Error", Constants.ResponseCodes.ConflictHubSendingFailed, "Sending failed");
        }
    }

    [HubMethodName("SendWebRTCAnswer")]
    public async Task SendWebRTCAnswerAsync(int meetingId, object answer)
    {
        try
        {
            var userId = await GetUserIdAsync();
            if (userId == 0)
                return;

            await Clients.Group(GetMeetingGroupName(meetingId)).SendAsync("ReceiveWebRTCAnswer", userId, answer);
        }
        catch
        {
            await Clients.Caller.SendAsync("Error", Constants.ResponseCodes.ConflictHubSendingFailed, "Sending failed");
        }
    }

    [HubMethodName("SendICECandidate")]
    public async Task SendICECandidateAsync(int meetingId, object candidate)
    {
        try
        {
            var userId = await GetUserIdAsync();
            if (userId == 0)
                return;

            await Clients.OthersInGroup(GetMeetingGroupName(meetingId)).SendAsync("ReceiveICECandidate", userId, candidate);
        }
        catch
        {
            await Clients.Caller.SendAsync("Error", Constants.ResponseCodes.ConflictHubSendingFailed, "Sending failed");
        }
    }

    private static string GetUserDisplayName(User user) => user.FullName ?? user.Email;

    private static string GetMeetingGroupName(int meetingId) => $"meeting_{meetingId}";

    private async Task<int> GetUserIdAsync()
    {
        if (Context.Items.TryGetValue("user_id", out var userIdObj) && userIdObj is int userId)
            return userId;

        await Clients.Caller.SendAsync("Error", "User ID not found in context");
        return 0;
    }
}