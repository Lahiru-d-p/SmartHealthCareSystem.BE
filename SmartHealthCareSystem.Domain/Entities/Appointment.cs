using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHealthCareSystem.Domain.Entities;
public class Appointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int FK_PatientId { get; set; }
    public virtual Patient Patient { get; set; }
    public virtual int FK_DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; }
}
