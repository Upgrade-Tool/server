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
    public DbSet<UserOffice> UserOffices { get; set; }
    public DbSet<Brand> Brands { get; set; }




    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Car value factor precision
        builder.Entity<Car>()
            .Property(e => e.CarValueFactor)
            .HasColumnType("numeric(3,2)");

        // Brand -> Car relationship
        builder.Entity<Car>()
            .HasOne(e => e.Brand)
            .WithMany(b => b.Cars)
            .HasForeignKey(e => e.BrandId);

        // Drivetrain and Transmission stored as strings
        builder.Entity<Car>()
            .Property(e => e.Drivetrain)
            .HasConversion<string>();

        builder.Entity<Car>()
            .Property(e => e.Transmission)
            .HasConversion<string>();

        // KioskState uses KioskId as primary key
        builder.Entity<KioskState>()
            .HasKey(e => e.KioskId);

        // Unique slug on KioskDisplay
        builder.Entity<KioskDisplay>()
            .HasIndex(e => e.Slug)
            .IsUnique();

        // UserOffice composite key
        builder.Entity<UserOffice>()
            .HasKey(e => new { e.UserId, e.OfficeId });
    }
}