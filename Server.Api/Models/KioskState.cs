using System.Text.Json;

namespace Server.Api.Models;

public class KioskState
{
    public Guid KioskId { get; set; }
    public KioskDisplay Kiosk { get; set; } = null!;

    public JsonDocument? DraftState { get; set; }
    public JsonDocument? PublishedState { get; set; }
    public int PublishedVersion { get; set; }
    public DateTime UpdatedAt { get; set; }
}