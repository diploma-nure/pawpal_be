namespace Application.Interfaces;

public interface IMeetingService
{
    Task<string> GetRoomAsync(int meetingId);

    Task DeleteRoomAsync(string roomName);

    (string Url, string RoomName, string Token) GenerateJoinInfo(string roomName, User user);
}
