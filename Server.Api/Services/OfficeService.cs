using Server.Api.DTOs.Offices;
using Server.Api.Models;
using Server.Api.Repositories;

namespace Server.Api.Services;

public class OfficeService
{
    private readonly IOfficeRepository _officeRepository;

    public OfficeService(IOfficeRepository officeRepository)
    {
        _officeRepository = officeRepository;
    }

    public async Task<List<OfficeResponse>> GetAllAsync(int page, int pageSize)
    {
        var offices = await _officeRepository.GetAllAsync(page, pageSize);
        return offices.Select(MapToResponse).ToList();
    }

    public async Task<OfficeResponse?> GetByIdAsync(Guid id)
    {
        var office = await _officeRepository.GetByIdAsync(id);
        if (office == null) return null;
        return MapToResponse(office);
    }

    public async Task<(bool success, string error, OfficeResponse? office)> CreateAsync(CreateOfficeRequest request)
    {
        var codeExists = await _officeRepository.CodeExistsAsync(request.Code);
        if (codeExists)
            return (false, $"Office with code '{request.Code}' already exists", null);

        var office = new Office
        {
            Id = Guid.NewGuid(),
            Code = request.Code,
            Name = request.Name,
            Address = request.Address,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _officeRepository.CreateAsync(office);
        return (true, string.Empty, MapToResponse(created));
    }

    public async Task<(bool success, string error, OfficeResponse? office)> UpdateAsync(Guid id, UpdateOfficeRequest request)
    {
        var office = await _officeRepository.GetByIdAsync(id);
        if (office == null)
            return (false, "Office not found", null);

        // Check code isn't taken by a DIFFERENT office
        var codeExists = await _officeRepository.CodeExistsAsync(request.Code);
        if (codeExists && office.Code != request.Code)
            return (false, $"Office with code '{request.Code}' already exists", null);

        office.Code = request.Code;
        office.Name = request.Name;
        office.Address = request.Address;
        office.UpdatedAt = DateTime.UtcNow;

        var updated = await _officeRepository.UpdateAsync(office);
        return (true, string.Empty, MapToResponse(updated));
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _officeRepository.DeleteAsync(id);
    }

    private static OfficeResponse MapToResponse(Office office) =>
        new(office.Id, office.Code, office.Name, office.Address, office.CreatedAt, office.UpdatedAt);
}