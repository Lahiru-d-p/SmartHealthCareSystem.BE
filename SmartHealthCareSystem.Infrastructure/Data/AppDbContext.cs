using Microsoft.EntityFrameworkCore;
using SmartHealthCareSystem.Domain.Entities;
namespace SmartHealthCareSystem.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }



    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.OwnsOne(p => p.Address, address =>
            {
                address.Property(a => a.Street);
                address.Property(a => a.City);
                address.Property(a => a.State);
                address.Property(a => a.ZipCode);
            });
        });
    }
}
