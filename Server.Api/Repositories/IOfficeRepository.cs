using Server.Api.Models;

namespace Server.Api.Repositories;

public interface IOfficeRepository
{
    Task<List<Office>> GetAllAsync(int page, int pageSize);
    Task<Office?> GetByIdAsync(Guid id);
    Task<Office> CreateAsync(Office office);
    Task<Office> UpdateAsync(Office office);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> CodeExistsAsync(string code);
}