using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Api.Models;

namespace Server.Api.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {   
    }


    public DbSet<Car> Cars { get; set; }
    public DbSet<CarGroup> CarGroups { get; set; }
    public DbSet<KioskDisplay> KioskDisplays { get; set; }
    public DbSet<KioskState> KioskStates { get; set; }
    public DbSet<Office> Offices { get; set; }



  protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

    builder.Entity<Car>()
        .Property(e => e.CarValueFactor)
        .HasColumnType("numeric(3,2)");

    builder.Entity<KioskState>()
        .HasKey(e => e.KioskId);
}
}