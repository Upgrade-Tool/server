namespace Server.Api.Models;

public class KioskDisplay
{
    public Guid Id { get; set; }
    public string Slug { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Location { get; set; }
    public DateTime? LastSeenAt { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Guid OfficeId { get; set; }
    public Office Office { get; set; } = null!;

    public KioskState? State { get; set; }
}