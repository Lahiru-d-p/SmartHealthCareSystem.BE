using SmartHealthCareSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHealthCareSystem.Domain.Models
{
    public class AppointmentInsertModel
    {
        public int FK_PatientId { get; set; }
        public virtual int FK_DoctorId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string? Description { get; set; }
    }

}
