namespace Server.Api.DTOs.Offices;

public record UpdateOfficeRequest(
    string Code,
    string Name,
    string Address
);