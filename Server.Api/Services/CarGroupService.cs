using Server.Api.Repositories;
using Server.Api.DTOs.CarGroups;
using Server.Api.Models;

namespace Server.Api.Services;

public class CarGroupService
{
    private readonly ICarGroupRepository _carGroupRepository;

    public CarGroupService(ICarGroupRepository carGroupRepository)
    {
        _carGroupRepository = carGroupRepository;
    }


    public async Task<List<CarGroupResponse>> GetAllAsync(int page, int pageSize)
    {
        var group = await _carGroupRepository.GetAllAsync(page, pageSize);
        return group.Select(MapToResponse).ToList();
    }

    public async Task<CarGroupResponse?> GetByIdAsync(Guid id)
    {
        var group = await _carGroupRepository.GetByIdAsync(id);
        if (group == null) return null;
        return MapToResponse(group);
    }

    public async Task<CarGroupResponse> CreateAsync(CreateCarGroupRequest request)
    {
        var group = new CarGroup
        {
            Id = Guid.NewGuid(),
            Code = request.Code,
            Name = request.Name,
            DrivetrainType = request.DrivetrainType,
            SortOrder = request.SortOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _carGroupRepository.CreateAsync(group);
        return MapToResponse(created);
    }
    
    public async Task<CarGroupResponse?> UpdateAsync(Guid id, UpdateCarGroupRequest request)
    {
        var group = await _carGroupRepository.GetByIdAsync(id);
        if (group == null) return null;

        group.Code = request.Code;
        group.Name = request.Name;
        group.DrivetrainType = request.DrivetrainType;
        group.SortOrder = request.SortOrder;
        group.UpdatedAt = DateTime.UtcNow;

        var updated = await _carGroupRepository.UpdateAsync(group);
        return MapToResponse(updated);
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _carGroupRepository.DeleteAsync(id);
    }



    private static CarGroupResponse MapToResponse(CarGroup group) =>
        new(group.Id, group.Code, group.Name, group.DrivetrainType, group.SortOrder, group.CreatedAt, group.UpdatedAt);
}