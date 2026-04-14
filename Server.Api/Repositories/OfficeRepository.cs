using Microsoft.EntityFrameworkCore;
using Server.Api.Data;
using Server.Api.Models;

namespace Server.Api.Repositories;

public class OfficeRepository : IOfficeRepository
{
    private readonly AppDbContext _db;

    public OfficeRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Office>> GetAllAsync(int page, int pageSize)
    {
        return await _db.Offices
            .OrderBy(o => o.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    
    public async Task<bool> CodeExistsAsync(string code)
    {
        return await _db.Offices.AnyAsync(o => o.Code == code);
    }

    public async Task<Office?> GetByIdAsync(Guid id)
    {
        return await _db.Offices
            .Include(o => o.KioskDisplays)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Office> CreateAsync(Office office)
    {
        _db.Offices.Add(office);
        await _db.SaveChangesAsync();
        return office;
    }

    public async Task<Office> UpdateAsync(Office office)
    {
        office.UpdatedAt = DateTime.UtcNow;
        _db.Offices.Update(office);
        await _db.SaveChangesAsync();
        return office;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var office = await _db.Offices.FindAsync(id);
        if (office == null) return false;

        _db.Offices.Remove(office);
        await _db.SaveChangesAsync();
        return true;
    }
}