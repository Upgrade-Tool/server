using Server.Api.Models;

namespace Server.Api.Repositories;

public interface ICarRepository
{
    Task<List<Car>> GetAllAsync(int page, int pageSize);
    Task<Car?> GetByIdAsync(Guid id);
    Task<Car> CreateAsync(Car car);
    Task<Car> UpdateAsync(Car car);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ToggleActiveAsync(Guid id);
}