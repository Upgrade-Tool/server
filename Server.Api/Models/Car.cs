using System.ComponentModel.DataAnnotations;

namespace Server.Api.Models;

public class Car
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
    public int Horsepower { get; set; }
    public int RangeKm { get; set; }
    public string Drivetrain { get; set; } = null!;
    public string Transmission { get; set; } = null!;
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