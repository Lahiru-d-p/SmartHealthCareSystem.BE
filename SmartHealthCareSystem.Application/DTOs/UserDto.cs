using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthCareSystem.Application.DTOs
{
    class UserDto
    {
	}
	public class UserViewModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string ContactNumber { get; set; }
		public string NIC { get; set; }
		public string ContactEMail { get; set; }
		public Address? Address { get; set; }
		public string Role { get; set; }
	}

	public class UserInsertModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string ContactNumber { get; set; }
		public string NIC { get; set; } 
		public string ContactEMail { get; set; }
		public Address? Address { get; set; }
		public string Password { get; set; }
	}
	public class UserUpdateModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string NIC { get; set; }
		public string ContactNumber { get; set; }
		public string ContactEMail { get; set; }
		public Address? Address { get; set; }
	}

	public class UserListModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class LoginRequestModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
	public class LoginResponseModel
	{
		public string Token { get; set; }
		public int UserId { get; set; }
		public string Role { get; set; }
	}

	public class PatientViewModel : UserViewModel
	{
		public DateTime DateOfBirth { get; set; }
		public string? MedicalHistory { get; set; }
	}
	public class PatientInsertModel : UserInsertModel
	{
		public DateTime DateOfBirth { get; set; }
		public string? MedicalHistory { get; set; }
	}


	public class PatientUpdateModel : UserUpdateModel
	{
		public DateTime DateOfBirth { get; set; }
		public string? MedicalHistory { get; set; }

	}
	public class DoctorViewModel : UserViewModel
	{
		public string Specialty { get; set; }
		public string LicenseNumber { get; set; }
		public string ClinicAddress { get; set; }
	}
	public class DoctorInsertModel : UserInsertModel
	{
		public string Specialty { get; set; }
		public string LicenseNumber { get; set; }
		public string ClinicAddress { get; set; }
	}
	public class DoctorUpdateModel : UserUpdateModel
	{
		public string Specialty { get; set; }
		public string LicenseNumber { get; set; }
		public string ClinicAddress { get; set; }

	}
	public class AvailableSlotesViewModel
	{
		public DayOfWeek DayOfWeek { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
	}
	public class AllAvailableSlotesViewModel : AvailableSlotesViewModel
	{
		public int Id { get; set; }
		public int DoctorId { get; set; }
	}

	public class DoctorTimeSloteInsertModel
	{
		public int DoctorId { get; set; }
		public DayOfWeek DayOfWeek { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
	}

	public class DoctorTimeSloteUpdateModel: DoctorTimeSloteInsertModel
	{
		public int Id { get; set; }
	}
}
