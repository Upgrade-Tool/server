using Microsoft.AspNetCore.SignalR;
using Server.Api.Hubs;
using Server.Api.Repositories;
using System.Text.Json;

namespace Server.Api.Services;

public class KioskStateService
{
    private readonly IKioskStateRepository _kioskStateRepository;
    private readonly IKioskDisplayRepository _kioskDisplayRepository;
    private readonly IHubContext<KioskHub> _hubContext;

    public KioskStateService(
        IKioskStateRepository kioskStateRepository,
        IKioskDisplayRepository kioskDisplayRepository,
        IHubContext<KioskHub> hubContext)
    {
        _kioskStateRepository = kioskStateRepository;
        _kioskDisplayRepository = kioskDisplayRepository;
        _hubContext = hubContext;
    }

    public async Task<(bool success, string error, JsonDocument? state)> GetStateAsync(string slug)
    {
        var kiosk = await _kioskDisplayRepository.GetBySlugAsync(slug);
        if (kiosk == null)
            return (false, "Kiosk not found", null);

        var state = await _kioskStateRepository.GetByKioskIdAsync(kiosk.Id);
        if (state?.PublishedState == null)
        {
            var idleState = JsonDocument.Parse("{\"displayMode\":\"idle\"}");
            return (true, string.Empty, idleState);
        }

        return (true, string.Empty, state.PublishedState);
    }

    public async Task<(bool success, string error)> SaveDraftAsync(string slug, JsonDocument state)
    {
        var kiosk = await _kioskDisplayRepository.GetBySlugAsync(slug);
        if (kiosk == null)
            return (false, "Kiosk not found");

        await _kioskStateRepository.UpsertDraftAsync(kiosk.Id, state);
        return (true, string.Empty);
    }

    public async Task<(bool success, string error)> PublishAsync(string slug, JsonDocument state)
    {
        var kiosk = await _kioskDisplayRepository.GetBySlugAsync(slug);
        if (kiosk == null)
            return (false, "Kiosk not found");

        await _kioskStateRepository.UpsertPublishedAsync(kiosk.Id, state);

        // Push to iPad via SignalR
        await _hubContext.Clients.Group(slug).SendAsync("StateUpdated", state);

        return (true, string.Empty);
    }

    public async Task<(bool success, string error)> ResetAsync(string slug)
    {
        var kiosk = await _kioskDisplayRepository.GetBySlugAsync(slug);
        if (kiosk == null)
            return (false, "Kiosk not found");

        await _kioskStateRepository.ResetAsync(kiosk.Id);

        // Push idle to iPad via SignalR
        await _hubContext.Clients.Group(slug).SendAsync("StateReset");

        return (true, string.Empty);
    }

    public async Task<(bool success, string error)> AcceptOfferAsync(string slug)
    {
        var kiosk = await _kioskDisplayRepository.GetBySlugAsync(slug);
        if (kiosk == null)
            return (false, "Kiosk not found");

        // Push accepted event to iPad via SignalR
        await _hubContext.Clients.Group(slug).SendAsync("OfferAccepted");

        return (true, string.Empty);
    }

    public async Task<(bool success, string error)> ShowcaseOpenAsync(string slug, Guid carId)
    {
        var kiosk = await _kioskDisplayRepository.GetBySlugAsync(slug);
        if (kiosk == null)
            return (false, "Kiosk not found");

        await _hubContext.Clients.Group(slug).SendAsync("ShowcaseOpen", carId);
        return (true, string.Empty);
    }

    public async Task<(bool success, string error)> ShowcaseCloseAsync(string slug)
    {
        var kiosk = await _kioskDisplayRepository.GetBySlugAsync(slug);
        if (kiosk == null)
            return (false, "Kiosk not found");

        await _hubContext.Clients.Group(slug).SendAsync("ShowcaseClose");
        return (true, string.Empty);
    }
}