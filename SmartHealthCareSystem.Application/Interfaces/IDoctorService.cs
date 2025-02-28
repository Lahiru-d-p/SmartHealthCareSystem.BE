using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthCareSystem.Application.Interfaces
{
    public interface IDoctorService
	{
		//Get All Doctors Details
		Task<List<DoctorViewModel>> GetAllDoctorsAsync();

		//Get All Doctors List
		Task<List<UserListModel>> GetAllDoctorsListAsync();
		//Get Doctor By Id
		Task<DoctorViewModel> GetDoctorByIdAsync(int id);
		//Add Doctor
		Task<DoctorViewModel> AddDoctorAsync(DoctorInsertModel doctor);
		//Update Doctor
		Task<DoctorViewModel> UpdateDoctorAsync(DoctorUpdateModel doctor);
		//Delete Doctor
		Task DeleteDoctorAsync(int id);
		//Get Doctor Available Time Slotes By Doctor Id
		Task<List<AllAvailableSlotesViewModel>> GetAllAvailableTimeSlotesByDoctorIdAsync(int id);
		Task<AllAvailableSlotesViewModel> AddAvailableTimeSloteAsync(DoctorTimeSloteInsertModel model);
		Task<AllAvailableSlotesViewModel> UpdateAvailableTimeSloteAsync(DoctorTimeSloteUpdateModel model);
		Task DeleteAvailableTimeSloteAsync(int id);
	}
}
