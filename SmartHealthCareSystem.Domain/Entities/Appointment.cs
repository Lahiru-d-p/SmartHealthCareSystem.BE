using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartHealthCareSystem.Domain.Entities;
public class Appointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
	public string AppointmentNumber { get; set; }
	public int FK_PatientId { get; set; }
    public virtual Patient Patient { get; set; }
    public virtual int FK_DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public int Status { get; set; }
    public string? Description { get; set; }
    public string? Prescription { get; set; }
}
