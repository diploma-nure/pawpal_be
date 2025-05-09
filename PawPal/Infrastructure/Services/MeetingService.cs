namespace Infrastructure.Services;

public class MeetingService(RoomServiceClient roomService, IOptions<LiveKitConfig> liveKitConfigOptions) : IMeetingService
{
    private readonly RoomServiceClient _roomService = roomService;
    private readonly LiveKitConfig _liveKitConfig = liveKitConfigOptions.Value;

    private const string RoomNamePrefix = "meeting-room";

    public async Task<string> GetRoomAsync(int meetingId)
    {
        var roomName = $"{RoomNamePrefix}-{meetingId}";

        if (await RoomExistsAsync(roomName))
            return roomName;

        var req = new CreateRoomRequest { Name = roomName };
        var room = await _roomService.CreateRoom(req);
        return room.Name;
    }

    public async Task DeleteRoomAsync(string roomName)
    {
        var req = new DeleteRoomRequest { Room = roomName };
        await _roomService.DeleteRoom(req);
    }

    public (string Url, string RoomName, string Token) GenerateJoinInfo(string roomName, User user)
    {
        var grants = new VideoGrants
        {
            RoomJoin = true,
            Room = roomName
        };

        var token = new AccessToken(_liveKitConfig.ApiKey, _liveKitConfig.ApiSecret)
            .WithIdentity(user.Email)
            .WithName(user.FullName ?? user.Email)
            .WithGrants(grants)
            .WithTtl(TimeSpan.FromHours(2));

        return (_liveKitConfig.Url, roomName, token.ToJwt());
    }

    private async Task<bool> RoomExistsAsync(string roomName)
    {
        var req = new ListRoomsRequest();
        var rooms = await _roomService.ListRooms(req);

        return rooms.Rooms.Any(r => r.Name == roomName);
    }
}
