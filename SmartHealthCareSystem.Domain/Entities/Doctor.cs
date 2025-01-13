using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHealthCareSystem.Domain.Entities;

public class Doctor
{
    public string Specialty { get; set; } 
    public string LicenseNumber { get; set; } 
    public string ClinicAddress { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
    public ICollection<DoctorAvailability>? AvailableTimes { get; set; }
}
