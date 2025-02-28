using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Application.Interfaces;
using SmartHealthCareSystem.Common.Utilities;
using SmartHealthCareSystem.Domain.Entities;
using SmartHealthCareSystem.Domain.Interfaces;
using System.Numerics;

namespace SmartHealthCareSystem.Application.Services;
public class DoctorService : IDoctorService
{
	private readonly IDoctorRepository _doctorRepository;

	public DoctorService(IDoctorRepository doctorRepository)
	{
		_doctorRepository = doctorRepository;
	}

	public async Task<List<DoctorViewModel>> GetAllDoctorsAsync()
	{
		try
		{
			var doctors = new List<DoctorViewModel>();
			var result = await _doctorRepository.GetAllDoctorsAsync();
			if (result != null)
			{
				doctors = result.Select(doctor => ConvertToDoctorViewModel(doctor)).ToList();
			}
			return doctors;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Doctors retrieval failed.", ex);
		}
	}

	public async Task<List<UserListModel>> GetAllDoctorsListAsync()
	{
		try
		{
			var doctors = new List<UserListModel>();
			var result = await _doctorRepository.GetAllDoctorsAsync();
			if (result != null)
			{
				doctors = result.Select(d => new UserListModel { Id = d.Id, Name = $"{d.FirstName} {d.LastName}" }).ToList();
			}
			return doctors;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Doctors List retrieval failed.", ex);
		}
	}
	public async Task<DoctorViewModel> GetDoctorByIdAsync(int id)
	{
		try
		{
			var doctor = await _doctorRepository.GetDoctorByIdAsync(id);
			if (doctor == null)
			{
				throw new KeyNotFoundException("Doctor not found.");
			}
			var doctorView = new DoctorViewModel();
			doctorView = ConvertToDoctorViewModel(doctor);
			return doctorView;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Doctor retrieval failed.", ex);
		}
	}
	public async Task<DoctorViewModel> AddDoctorAsync(DoctorInsertModel doctorModel)
	{
		try
		{
			if (await _doctorRepository.IsDoctorWithEmailAsync(doctorModel.ContactEMail))
			{
				throw new InvalidOperationException("A doctor with the provided email already exists.");
			}
			var hashedPassword = PasswordHasherHelper.HashPassword(doctorModel.Password);
			var doctor = new Doctor
			{
				NIC = doctorModel.NIC,
				FirstName = doctorModel.FirstName,
				LastName = doctorModel.LastName,
				ContactEMail = doctorModel.ContactEMail,
				ContactNumber = doctorModel.ContactNumber,
				PasswordHash = hashedPassword,
				Role = "Doctor",
				Address = doctorModel.Address,
				Specialty = doctorModel.Specialty,
				LicenseNumber = doctorModel.LicenseNumber,
				ClinicAddress = doctorModel.ClinicAddress,

			};
			doctor = await _doctorRepository.AddDoctorAsync(doctor);

			var doctorView = new DoctorViewModel();
			doctorView = ConvertToDoctorViewModel(doctor);
			return doctorView;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Doctor Insert failed.", ex);
		}
	}
	public async Task<DoctorViewModel> UpdateDoctorAsync(DoctorUpdateModel doctorModel)
	{
		try
		{
			var doctor = await _doctorRepository.GetDoctorByIdAsync(doctorModel.Id);
			if (doctor == null)
			{
				throw new KeyNotFoundException("Doctor not found.");
			}

			doctor.ContactNumber = doctorModel.ContactNumber;
			doctor.NIC = doctorModel.NIC;
			doctor.Address = doctorModel.Address;
			doctor.ClinicAddress = doctorModel.ClinicAddress;
			doctor.FirstName = doctorModel.FirstName;
			doctor.ContactEMail = doctorModel.ContactEMail;
			doctor.LastName = doctorModel.LastName;
			doctor.LicenseNumber = doctorModel.LicenseNumber;
			doctor.Specialty = doctorModel.Specialty;

			await _doctorRepository.UpdateDoctorAsync(doctor);


			var doctorView = new DoctorViewModel();
			doctorView = ConvertToDoctorViewModel(doctor);
			return doctorView;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Doctor update failed.", ex);
		}
	}

	public async Task DeleteDoctorAsync(int id)
	{
		try
		{
			var doctor = await _doctorRepository.GetDoctorByIdAsync(id);
			if (doctor == null)
			{
				throw new KeyNotFoundException("Doctor not found.");
			}

			await _doctorRepository.DeleteDoctorAsync(doctor);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Doctor deletion failed.", ex);
		}
	}

	public async Task<List<AllAvailableSlotesViewModel>> GetAllAvailableTimeSlotesByDoctorIdAsync(int id)
	{
		try
		{
			var times = new List<AllAvailableSlotesViewModel>();
			var result = await _doctorRepository.GetAllAvailableTimeSlotesByDoctorIdAsync(id);
			if (result != null)
			{
				times = result.Select(slot => ConvertToAllAvailableSlotesViewModel(slot)).ToList();
			}
			return times;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Time Slotes retrieval failed.", ex);
		}

	}

	public async Task<AllAvailableSlotesViewModel> AddAvailableTimeSloteAsync(DoctorTimeSloteInsertModel model)
	{
		try
		{
			if (await _doctorRepository.IsTimeSloteOverlappingAsync(model.DoctorId,model.DayOfWeek,model.StartTime,model.EndTime))
			{
				throw new InvalidOperationException("Time Slote is overlpping.");
			}

			var item = new DoctorAvailability
			{
				FK_DoctorId = model.DoctorId,
				DayOfWeek = model.DayOfWeek,
				StartTime = model.StartTime,
				EndTime = model.EndTime
			};
			item = await _doctorRepository.AddDoctorAvailableSlotAsync(item);

			var slotesView = new AllAvailableSlotesViewModel();
			slotesView = ConvertToAllAvailableSlotesViewModel(item);
			return slotesView;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Time Slote Insert failed.", ex);
		}
	}
	public async Task<AllAvailableSlotesViewModel> UpdateAvailableTimeSloteAsync(DoctorTimeSloteUpdateModel model)
	{
		try
		{
			var availability = await _doctorRepository.GetDoctorAvailabilityByIdAsync(model.Id);
			if (availability == null)
			{
				throw new KeyNotFoundException("Time Slote not found.");
			}
			else if (availability.FK_DoctorId!=model.DoctorId)
			{
				throw new InvalidOperationException("Assigned doctor cant be changed.");
			}

			availability.DayOfWeek = model.DayOfWeek;
			availability.StartTime = model.StartTime;
			availability.EndTime = model.EndTime;

			await _doctorRepository.UpdateDoctorAvailableSlotAsync(availability);


			var slotesView = new AllAvailableSlotesViewModel();
			slotesView = ConvertToAllAvailableSlotesViewModel(availability);
			return slotesView;
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Time Slote update failed.", ex);
		}
	}

	public async Task DeleteAvailableTimeSloteAsync(int id)
	{
		try
		{
			var availability = await _doctorRepository.GetDoctorAvailabilityByIdAsync(id);
			if (availability == null)
			{
				throw new KeyNotFoundException("Time Slote not found.");
			}

			await _doctorRepository.DeleteDoctorAvailableSlotAsync(availability);
		}
		catch (Exception ex)
		{
			throw new InvalidOperationException("Time Slote deletion failed.", ex);
		}
	}
	#region Helper methods
	private DoctorViewModel ConvertToDoctorViewModel(Doctor doctor)
	{
		return new DoctorViewModel
		{
			Id = doctor.Id,
			FirstName = doctor.FirstName,
			LastName = doctor.LastName,
			Address = doctor.Address,
			ClinicAddress = doctor.ClinicAddress,
			ContactEMail = doctor.ContactEMail,
			ContactNumber = doctor.ContactNumber,
			LicenseNumber = doctor.LicenseNumber,
			NIC = doctor.NIC,
			Role = doctor.Role,
			Specialty = doctor.Specialty
		};
	}
	private AllAvailableSlotesViewModel ConvertToAllAvailableSlotesViewModel(DoctorAvailability doctorAvailability)
	{
		return new AllAvailableSlotesViewModel
		{
			Id = doctorAvailability.Id,
			DoctorId = doctorAvailability.FK_DoctorId,
			DayOfWeek = doctorAvailability.DayOfWeek,
			StartTime  = doctorAvailability.StartTime,
			EndTime = doctorAvailability.EndTime
		};
	}

	#endregion
}