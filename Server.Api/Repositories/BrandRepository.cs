using Microsoft.EntityFrameworkCore;
using Server.Api.Data;
using Server.Api.Models;

namespace Server.Api.Repositories;



public class BrandRepository : IBrandRepository
{

    private readonly AppDbContext _db;
    public BrandRepository(AppDbContext db)
    {
        _db = db;
    }


    public async Task<List<Brand>> GetAllAsync()
    {
        return await _db.Brands.OrderBy(b => b.Name).ToListAsync();
    }

    public async Task<Brand?> GetByIdAsync(Guid id)
    {
        return await _db.Brands.FindAsync(id);
    }

    public async Task<Brand> CreateAsync(Brand brand)
    {
        _db.Brands.Add(brand);
        await _db.SaveChangesAsync();
        return brand;
    }

    public async Task<Brand> UpdateAsync(Brand brand)
    {
        brand.UpdatedAt = DateTime.UtcNow;
        _db.Brands.Update(brand);
        await _db.SaveChangesAsync();
        return brand;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var brand = await _db.Brands.FindAsync(id);
        if (brand == null)
        {
            return false;
        }
         _db.Brands.Remove(brand);
         await _db.SaveChangesAsync();
         return true;
    }
}