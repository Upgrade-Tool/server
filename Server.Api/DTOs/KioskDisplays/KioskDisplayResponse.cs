namespace Server.Api.DTOs.KioskDisplays;

public record KioskDisplayResponse(
    Guid Id,
    string Slug,
    string Name,
    string? Location,
    Guid OfficeId,
    string OfficeName,
    bool IsActive,
    DateTime? LastSeenAt,
    DateTime CreatedAt,
    DateTime UpdatedAt
);