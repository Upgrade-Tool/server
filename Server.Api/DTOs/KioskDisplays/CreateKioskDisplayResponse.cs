namespace Server.Api.DTOs.KioskDisplays;

public record CreateKioskDisplayRequest(
    string Slug,
    string Name,
    string? Location,
    Guid OfficeId
);