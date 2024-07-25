using Microsoft.EntityFrameworkCore;
using VehicleWiki.Model;

namespace VehicleWiki.DAL;

public class VehicleDbContext : DbContext
{
    public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
    {

    }

    public VehicleDbContext()
    {
    }

    public DbSet<VehicleMake> VehicleMakes { get; set; }
    public DbSet<VehicleModel> VehicleModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VehicleMake>()
        .HasMany(x => x.Models)
        .WithOne(x => x.Make)
        .HasForeignKey(x => x.MakeId);
    }
}
