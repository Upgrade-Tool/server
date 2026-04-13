using Server.Api.Models.Enums;

namespace Server.Api.DTOs.Cars;

public record CreateCarRequest(
    string Name,
    Guid BrandId,
    Guid GroupId,
    int Horsepower,
    int RangeKm,
    Drivetrain Drivetrain,
    Transmission Transmission,
    decimal CarValueFactor,
    string? ImageUrlSideLeft,
    string? ImageUrlSideRight,
    string? ImageUrlDisplay
);