using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHealthCareSystem.Domain.Entities;

public class DoctorAvailability
{
    public int Id { get; set; }
    public int FK_DoctorId { get; set; }
    public virtual Doctor Doctor { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; } 
    public TimeSpan EndTime { get; set; }
}
