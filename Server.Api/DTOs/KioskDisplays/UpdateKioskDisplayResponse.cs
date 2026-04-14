namespace Server.Api.DTOs.KioskDisplays;

public record UpdateKioskDisplayRequest(
    string Name,
    string? Location,
    Guid OfficeId
);