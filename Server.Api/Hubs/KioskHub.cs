using Microsoft.AspNetCore.SignalR;


namespace Server.Api.Hubs;

public class KioskHub : Hub
{
    public async Task JoinKiosk(String kioskSlug)  
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, kioskSlug);
    }

    public async Task LeaveKiosk(String kioskSlug) {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, kioskSlug);
    }
}