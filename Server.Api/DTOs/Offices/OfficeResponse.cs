namespace Server.Api.DTOs.Offices;

public record OfficeResponse(
    Guid Id,
    string Code,
    string Name,
    string Address,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

