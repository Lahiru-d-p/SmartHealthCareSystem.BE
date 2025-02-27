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
		Task<Doctor> AddDoctorAsync(DoctorInsertModel doctor);
		//Update Doctor
		Task<Doctor> UpdateDoctorAsync(DoctorUpdateModel doctor);
		//Delete Doctor
		Task DeleteDoctorAsync(int id);
	}
}
