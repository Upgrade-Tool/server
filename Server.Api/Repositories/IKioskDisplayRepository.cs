using Server.Api.Models;

namespace Server.Api.Repositories;

public interface IKioskDisplayRepository
{
    Task<List<KioskDisplay>> GetAllAsync(int page, int pageSize);
    Task<KioskDisplay?> GetByIdAsync(Guid id);
    Task<KioskDisplay?> GetBySlugAsync(string slug);
    Task<bool> SlugExistsAsync(string slug);
    Task<KioskDisplay> CreateAsync(KioskDisplay kioskDisplay);
    Task<KioskDisplay> UpdateAsync(KioskDisplay kioskDisplay);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ToggleActiveAsync(Guid id);
}