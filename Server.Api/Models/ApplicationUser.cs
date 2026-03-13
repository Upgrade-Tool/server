using Microsoft.AspNetCore.Identity;

namespace Server.Api.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? DisplayName { get; set; }
    public ICollection<UserOffice> UserOffices { get; set; } = new List<UserOffice>();
}