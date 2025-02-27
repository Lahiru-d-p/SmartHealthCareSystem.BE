
namespace SmartHealthCareSystem.Application.DTOs
{
    class AppointmentDto
    {
    }
	public class AppointmentViewModel
	{
		public int Id { get; set; }
		public string AppointmentNumber { get; set; }
		public int FK_PatientId { get; set; }
		public PatientViewModel Patient { get; set; }
		public int FK_DoctorId { get; set; }
		public DoctorViewModel Doctor { get; set; }
		public DateTime AppointmentDateTime { get; set; }
		public string? Description { get; set; }
		public string? Prescription { get; set; }
		public int Status { get; set; }
		public string StatusDescription { get; set; }
	}
	public class AppointmentListModel
	{
		public int Id { get; set; }
		public string AppointmentNumber { get; set; }
	}
	public class AppointmentInsertModel
	{
		public int FK_PatientId { get; set; }
		public bool ReAppointment { get; set; }
		public virtual int FK_DoctorId { get; set; }
		public DateTime AppointmentDateTime { get; set; }
		public string? Description { get; set; }
	}

	public class AppointmentUpdateModel
	{
		public int Id { get; set; }
		public int FK_PatientId { get; set; }
		public virtual int FK_DoctorId { get; set; }
		public DateTime AppointmentDateTime { get; set; }
		public string? Description { get; set; }
	}

	public class AppointmentPrescriptionUpdateModel
	{
		public int Id { get; set; }
		public string? Prescription { get; set; }
	}

	public class AppointmentStatusUpdateModel
	{
		public int Id { get; set; }
		public string Status { get; set; }
	}

}
