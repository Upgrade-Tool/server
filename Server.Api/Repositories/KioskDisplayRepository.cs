using Microsoft.EntityFrameworkCore;
using Server.Api.Data;
using Server.Api.Models;

namespace Server.Api.Repositories;

public class KioskDisplayRepository : IKioskDisplayRepository
{
    private readonly AppDbContext _db;

    public KioskDisplayRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<KioskDisplay>> GetAllAsync(int page, int pageSize)
    {
        return await _db.KioskDisplays
            .Include(k => k.Office)
            .Include(k => k.State)
            .OrderBy(k => k.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<KioskDisplay?> GetByIdAsync(Guid id)
    {
        return await _db.KioskDisplays
            .Include(k => k.Office)
            .Include(k => k.State)
            .FirstOrDefaultAsync(k => k.Id == id);
    }

    public async Task<KioskDisplay?> GetBySlugAsync(string slug)
    {
        return await _db.KioskDisplays
            .Include(k => k.Office)
            .Include(k => k.State)
            .FirstOrDefaultAsync(k => k.Slug == slug);
    }

    public async Task<bool> SlugExistsAsync(string slug)
    {
        return await _db.KioskDisplays.AnyAsync(k => k.Slug == slug);
    }

    public async Task<KioskDisplay> CreateAsync(KioskDisplay kioskDisplay)
    {
        _db.KioskDisplays.Add(kioskDisplay);
        await _db.SaveChangesAsync();
        return kioskDisplay;
    }

    public async Task<KioskDisplay> UpdateAsync(KioskDisplay kioskDisplay)
    {
        kioskDisplay.UpdatedAt = DateTime.UtcNow;
        _db.KioskDisplays.Update(kioskDisplay);
        await _db.SaveChangesAsync();
        return kioskDisplay;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var kiosk = await _db.KioskDisplays.FindAsync(id);
        if (kiosk == null) return false;

        _db.KioskDisplays.Remove(kiosk);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ToggleActiveAsync(Guid id)
    {
        var kiosk = await _db.KioskDisplays.FindAsync(id);
        if (kiosk == null) return false;

        kiosk.IsActive = !kiosk.IsActive;
        kiosk.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return true;
    }
}