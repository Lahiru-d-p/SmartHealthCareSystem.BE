

namespace SmartHealthCareSystem.Domain.Entities;

public class Doctor :User
{
    public string Specialty { get; set; } 
    public string LicenseNumber { get; set; } 
    public string ClinicAddress { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<DoctorAvailability>? AvailableTimes { get; set; }
}
