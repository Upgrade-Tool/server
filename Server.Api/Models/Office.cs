

namespace Server.Api.Models;

public class Office
{
    public Guid Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<KioskDisplay> KioskDisplays { get; set; } = new List<KioskDisplay>();
}