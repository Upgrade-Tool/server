namespace Server.Api.DTOs.CarGroups;

public record CarGroupResponse(
    Guid Id,
    string Code,
    string Name,
    string DrivetrainType,
    int SortOrder,
    DateTime CreatedAt,
    DateTime UpdatedAt
);