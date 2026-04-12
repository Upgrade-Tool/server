using Microsoft.EntityFrameworkCore;
using Server.Api.Data;
using Server.Api.Models;

namespace Server.Api.Repositories;

public class CarGroupRepository : ICarGroupRepository
{
    private readonly AppDbContext _db;
    
    public CarGroupRepository(AppDbContext db)
    {
        _db = db;
    }
    
    public async Task<List<CarGroup>> GetAllAsync(int page, int pageSize)
    {
        return await _db.CarGroups.OrderBy(c => c.Name).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<CarGroup?> GetByIdAsync(Guid id)
    {
        return await _db.CarGroups.FindAsync(id);
    }

    public async Task<CarGroup> CreateAsync(CarGroup carGroup)
    {
        _db.CarGroups.Add(carGroup);
        await _db.SaveChangesAsync();
        return carGroup;
    }

    public async Task<CarGroup> UpdateAsync(CarGroup carGroup)
    {
        carGroup.UpdatedAt = DateTime.UtcNow;
         _db.CarGroups.Update(carGroup);
        await _db.SaveChangesAsync();
        return carGroup;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var cargroup = await _db.CarGroups.FindAsync(id);
        if (cargroup == null)
        {
            return false;
        }
        _db.CarGroups.Remove(cargroup);
        await _db.SaveChangesAsync();
        return true;
    }
}