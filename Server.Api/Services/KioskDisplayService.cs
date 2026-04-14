using Server.Api.DTOs.KioskDisplays;
using Server.Api.Models;
using Server.Api.Repositories;

namespace Server.Api.Services;

public class KioskDisplayService
{
    private readonly IKioskDisplayRepository _kioskDisplayRepository;
    private readonly IOfficeRepository _officeRepository;

    public KioskDisplayService(
        IKioskDisplayRepository kioskDisplayRepository,
        IOfficeRepository officeRepository)
    {
        _kioskDisplayRepository = kioskDisplayRepository;
        _officeRepository = officeRepository;
    }

    public async Task<List<KioskDisplayResponse>> GetAllAsync(int page, int pageSize)
    {
        var kiosks = await _kioskDisplayRepository.GetAllAsync(page, pageSize);
        return kiosks.Select(MapToResponse).ToList();
    }

    public async Task<KioskDisplayResponse?> GetByIdAsync(Guid id)
    {
        var kiosk = await _kioskDisplayRepository.GetByIdAsync(id);
        if (kiosk == null) return null;
        return MapToResponse(kiosk);
    }

    public async Task<KioskDisplayResponse?> GetBySlugAsync(string slug)
    {
        var kiosk = await _kioskDisplayRepository.GetBySlugAsync(slug);
        if (kiosk == null) return null;
        return MapToResponse(kiosk);
    }

    public async Task<(bool success, string error, KioskDisplayResponse? kiosk)> CreateAsync(CreateKioskDisplayRequest request)
    {
        var slugExists = await _kioskDisplayRepository.SlugExistsAsync(request.Slug);
        if (slugExists)
            return (false, $"A kiosk with slug '{request.Slug}' already exists", null);

        var officeExists = await _officeRepository.GetByIdAsync(request.OfficeId);
        if (officeExists == null)
            return (false, "Office not found", null);

        var kiosk = new KioskDisplay
        {
            Id = Guid.NewGuid(),
            Slug = request.Slug,
            Name = request.Name,
            Location = request.Location,
            OfficeId = request.OfficeId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _kioskDisplayRepository.CreateAsync(kiosk);
        var result = await _kioskDisplayRepository.GetByIdAsync(created.Id);
        return (true, string.Empty, MapToResponse(result!));
    }

    public async Task<(bool success, string error, KioskDisplayResponse? kiosk)> UpdateAsync(Guid id, UpdateKioskDisplayRequest request)
    {
        var kiosk = await _kioskDisplayRepository.GetByIdAsync(id);
        if (kiosk == null)
            return (false, "Kiosk not found", null);

        var officeExists = await _officeRepository.GetByIdAsync(request.OfficeId);
        if (officeExists == null)
            return (false, "Office not found", null);

        kiosk.Name = request.Name;
        kiosk.Location = request.Location;
        kiosk.OfficeId = request.OfficeId;
        kiosk.UpdatedAt = DateTime.UtcNow;

        var updated = await _kioskDisplayRepository.UpdateAsync(kiosk);
        var result = await _kioskDisplayRepository.GetByIdAsync(updated.Id);
        return (true, string.Empty, MapToResponse(result!));
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _kioskDisplayRepository.DeleteAsync(id);
    }

    public async Task<bool> ToggleActiveAsync(Guid id)
    {
        return await _kioskDisplayRepository.ToggleActiveAsync(id);
    }

    private static KioskDisplayResponse MapToResponse(KioskDisplay kiosk) =>
        new(
            kiosk.Id,
            kiosk.Slug,
            kiosk.Name,
            kiosk.Location,
            kiosk.OfficeId,
            kiosk.Office.Name,
            kiosk.IsActive,
            kiosk.LastSeenAt,
            kiosk.CreatedAt,
            kiosk.UpdatedAt
        );
}