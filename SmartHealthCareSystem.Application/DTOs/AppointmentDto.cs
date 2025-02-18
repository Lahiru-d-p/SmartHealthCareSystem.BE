
namespace SmartHealthCareSystem.Application.DTOs
{
    class AppointmentDto
    {
    }
	public class AppointmentInsertModel
	{
		public int FK_PatientId { get; set; }
		public virtual int FK_DoctorId { get; set; }
		public DateTime AppointmentDateTime { get; set; }
		public string? Description { get; set; }
	}

}
