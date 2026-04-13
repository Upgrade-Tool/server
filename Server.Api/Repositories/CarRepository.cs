using Microsoft.EntityFrameworkCore;
using Server.Api.Data;
using Server.Api.Models;

namespace Server.Api.Repositories;

public class CarRepository : ICarRepository
{
    private readonly AppDbContext _db;

    public CarRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Car>> GetAllAsync(int page, int pageSize)
    {
        return await _db.Cars
            .Include(c => c.Brand)
            .Include(c => c.Group)
            .OrderBy(c => c.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Car?> GetByIdAsync(Guid id)
    {
        return await _db.Cars
            .Include(c => c.Brand)
            .Include(c => c.Group)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Car> CreateAsync(Car car)
    {
        _db.Cars.Add(car);
        await _db.SaveChangesAsync();
        return car;
    }

    public async Task<Car> UpdateAsync(Car car)
    {
        car.UpdatedAt = DateTime.UtcNow;
        _db.Cars.Update(car);
        await _db.SaveChangesAsync();
        return car;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var car = await _db.Cars.FindAsync(id);
        if (car == null) return false;

        _db.Cars.Remove(car);
        await _db.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> ToggleActiveAsync(Guid id)
    {
        var car = await _db.Cars.FindAsync(id);
        if (car == null) return false;

        car.IsActive = !car.IsActive;
        car.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return true;
    }
}