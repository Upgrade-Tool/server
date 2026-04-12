namespace Server.Api.DTOs.CarGroups;

public record CreateCarGroupRequest(
    string Code,
    string Name,
    string DrivetrainType,
    int SortOrder
);