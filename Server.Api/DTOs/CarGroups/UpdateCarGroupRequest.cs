namespace Server.Api.DTOs.CarGroups;

public record UpdateCarGroupRequest(
    string Code,
    string Name,
    string DrivetrainType,
    int SortOrder
);