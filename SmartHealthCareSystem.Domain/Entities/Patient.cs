namespace SmartHealthCareSystem.Domain.Entities;

public class Patient :User
{
    public DateTime DateOfBirth { get; set; }
    public string? MedicalHistory { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
}
