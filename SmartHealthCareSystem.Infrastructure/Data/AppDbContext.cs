using Microsoft.EntityFrameworkCore;
using SmartHealthCareSystem.Domain.Entities;
namespace SmartHealthCareSystem.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasDiscriminator<string>("UserType")
            .HasValue<Patient>("Patient")
            .HasValue<Doctor>("Doctor");

        modelBuilder.Entity<User>(entity =>
        {
            entity.OwnsOne(p => p.Address, address =>
            {
                address.Property(a => a.Street);
                address.Property(a => a.City);
                address.Property(a => a.State);
                address.Property(a => a.ZipCode);
            });
        });

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.FK_PatientId);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.FK_DoctorId);

        modelBuilder.Entity<DoctorAvailability>()
            .HasOne(da => da.Doctor)
            .WithMany(d => d.AvailableTimes)
            .HasForeignKey(da => da.FK_DoctorId);
    }
}
