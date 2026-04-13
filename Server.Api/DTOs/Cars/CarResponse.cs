using Server.Api.Models.Enums;

namespace Server.Api.DTOs.Cars;

public record CarResponse(
    Guid Id,
    string Name,
    Guid BrandId,
    string BrandName,
    Guid GroupId,
    string GroupName,
    int Horsepower,
    int RangeKm,
    Drivetrain Drivetrain,
    Transmission Transmission,
    decimal CarValueFactor,
    string? ImageUrlSideLeft,
    string? ImageUrlSideRight,
    string? ImageUrlDisplay,
    bool IsActive,
    DateTime CreatedAt,
    DateTime UpdatedAt
);