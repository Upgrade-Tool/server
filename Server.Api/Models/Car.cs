using Server.Api.Models.Enums;

namespace Server.Api.Models;

public class Car
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
    public int Horsepower { get; set; }
    public int RangeKm { get; set; }
    public Drivetrain Drivetrain { get; set; }
    public Transmission Transmission { get; set; }
    public decimal CarValueFactor { get; set; }
    public string? ImageUrlSideLeft { get; set; }
    public string? ImageUrlSideRight { get; set; }
    public string? ImageUrlDisplay { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Guid GroupId { get; set; }
    public CarGroup Group { get; set; } = null!;
}