using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthCareSystem.Application.DTOs
{
    class UserDto
    {
	}
	public class UserInsertModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string ContactNumber { get; set; }
		public string ContactEMail { get; set; }
		public Address? Address { get; set; }
		public string Password { get; set; }
	}

	public class PatientInsertModel : UserInsertModel
	{
		public DateTime DateOfBirth { get; set; }
		public string? MedicalHistory { get; set; }
	}

	public class DoctorInsertModel : UserInsertModel
	{
		public string Specialty { get; set; }
		public string LicenseNumber { get; set; }
		public string ClinicAddress { get; set; }
	}

	public class LoginRequestModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
