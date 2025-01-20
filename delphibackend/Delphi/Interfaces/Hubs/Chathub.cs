using Microsoft.AspNetCore.SignalR;

namespace delphibackend.Delphi.Interfaces.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string roomId, string participantId, string message)
    {
        await Clients.Group(roomId).SendAsync("ReceiveMessage", participantId, message);

        _ = Task.Run(async () =>
        {
            await Task.Delay(50000); // 50 segundos
            await Clients.Group(roomId).SendAsync("RemoveMessage", participantId, message);
        });

        Console.WriteLine($"Message sent to room {roomId} by participant {participantId}");
    }

    public async Task JoinRoom(string roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        Console.WriteLine($"Connection {Context.ConnectionId} joined room {roomId}");
    }

    public async Task LeaveRoom(string roomId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        Console.WriteLine($"Connection {Context.ConnectionId} left room {roomId}");
    }
}