using Microsoft.EntityFrameworkCore;
using Server.Api.Data;
using Server.Api.Models;
using System.Text.Json;

namespace Server.Api.Repositories;

public class KioskStateRepository : IKioskStateRepository
{
    private readonly AppDbContext _db;

    public KioskStateRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<KioskState?> GetByKioskIdAsync(Guid kioskId)
    {
        return await _db.KioskStates
            .FirstOrDefaultAsync(s => s.KioskId == kioskId);
    }

    public async Task UpsertDraftAsync(Guid kioskId, JsonDocument state)
    {
        var existing = await _db.KioskStates
            .FirstOrDefaultAsync(s => s.KioskId == kioskId);

        if (existing == null)
        {
            _db.KioskStates.Add(new KioskState
            {
                KioskId = kioskId,
                DraftState = state,
                PublishedVersion = 0,
                UpdatedAt = DateTime.UtcNow
            });
        }
        else
        {
            existing.DraftState = state;
            existing.UpdatedAt = DateTime.UtcNow;
        }

        await _db.SaveChangesAsync();
    }

    public async Task UpsertPublishedAsync(Guid kioskId, JsonDocument state)
    {
        var existing = await _db.KioskStates
            .FirstOrDefaultAsync(s => s.KioskId == kioskId);

        if (existing == null)
        {
            _db.KioskStates.Add(new KioskState
            {
                KioskId = kioskId,
                PublishedState = state,
                PublishedVersion = 1,
                UpdatedAt = DateTime.UtcNow
            });
        }
        else
        {
            existing.PublishedState = state;
            existing.PublishedVersion++;
            existing.UpdatedAt = DateTime.UtcNow;
        }

        await _db.SaveChangesAsync();
    }

    public async Task ResetAsync(Guid kioskId)
    {
        var existing = await _db.KioskStates
            .FirstOrDefaultAsync(s => s.KioskId == kioskId);

        if (existing == null) return;

        existing.DraftState = null;
        existing.PublishedState = null;
        existing.PublishedVersion = 0;
        existing.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
    }
}