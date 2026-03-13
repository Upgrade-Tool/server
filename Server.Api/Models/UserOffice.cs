namespace Server.Api.Models;

public class UserOffice
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public Guid OfficeId { get; set; }
    public Office Office { get; set; } = null!;
}