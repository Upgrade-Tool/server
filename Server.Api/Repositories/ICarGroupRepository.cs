using Server.Api.Models;

namespace Server.Api.Repositories;

public interface ICarGroupRepository
{
    Task<List<CarGroup>> GetAllAsync(int page, int pageSize);
    Task<CarGroup?> GetByIdAsync(Guid id);
    
    Task<CarGroup> CreateAsync(CarGroup carGroup);
    
    Task<CarGroup> UpdateAsync(CarGroup carGroup);

    Task<bool> DeleteAsync(Guid id);

}