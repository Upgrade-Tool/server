using Server.Api.Models;

namespace Server.Api.Repositories;

public interface IBrandRepository
{
    Task<List<Brand>> GetAllAsync(int page, int pageSize);
    Task<Brand?> GetByIdAsync(Guid id);
    Task<Brand> CreateAsync(Brand brand);
    Task<Brand> UpdateAsync(Brand brand);
    Task<bool> DeleteAsync(Guid id);
}
