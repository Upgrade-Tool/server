using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Server.Api.Data;

namespace Server.Api.Hubs;

public class KioskHub : Hub
{
    private readonly AppDbContext _db;

    public KioskHub(AppDbContext db)
    {
        _db = db;
    }

    public async Task JoinKiosk(string slug)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, slug);

        // Update LastSeenAt when iPad connects
        var kiosk = await _db.KioskDisplays
            .FirstOrDefaultAsync(k => k.Slug == slug);

        if (kiosk != null)
        {
            kiosk.LastSeenAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
    }

    public async Task LeaveKiosk(string slug)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, slug);
    }
}