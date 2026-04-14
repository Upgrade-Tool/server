namespace Server.Api.DTOs.Offices;

public record CreateOfficeRequest(
    string Code,
    string Name,
    string Address
);