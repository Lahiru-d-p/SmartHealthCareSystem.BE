using Microsoft.EntityFrameworkCore;
using SmartHealthCareSystem.Domain.Entities;
using SmartHealthCareSystem.Domain.Interfaces;
using SmartHealthCareSystem.Infrastructure.Data;

namespace SmartHealthCareSystem.Infrastructure.Repositories
{
    class DoctorRepository : IDoctorRepository
	{
		private readonly AppDbContext _context;

		public DoctorRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Doctor> GetDoctorByIdAsync(int id) => await _context.Doctors.FindAsync(id);
		public async Task<bool> IsDoctorWithEmailAsync(string email)
		{
			return await _context.Doctors.AnyAsync(u => u.ContactEMail == email);
		}		
		public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync() => await _context.Doctors.ToListAsync();
		public async Task<Doctor> AddDoctorAsync(Doctor doctor) 
		{
			await _context.Doctors.AddAsync(doctor); 
			await _context.SaveChangesAsync(); 
			return doctor; 
		}
		public async Task<DoctorAvailability> GetDoctorAvailabilityByIdAsync(int id) => await _context.DoctorAvailabilities.FindAsync(id);
		public async Task UpdateDoctorAsync(Doctor doctor) 
		{ 
			_context.Doctors.Update(doctor);
			await _context.SaveChangesAsync(); 
		}
		public async Task DeleteDoctorAsync(Doctor doctor)
		{			
			_context.Doctors.Remove(doctor);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<DoctorAvailability>> GetAllAvailableTimeSlotesByDoctorIdAsync(int id)
		{
			return await _context.DoctorAvailabilities.Where(u => u.Id == id).ToListAsync();
		}
		public async Task<bool> IsTimeSloteOverlappingAsync(int doctorId, DayOfWeek day, TimeSpan startTime, TimeSpan endTime)
		{
			return await _context.DoctorAvailabilities.AnyAsync(u =>
				u.FK_DoctorId == doctorId &&
				u.DayOfWeek == day &&
				(
					(u.StartTime < endTime && u.EndTime > startTime) ||
					(u.StartTime < endTime && u.StartTime >= startTime) ||
					(u.EndTime > startTime && u.EndTime <= endTime)
				)
			);
		}


		public async Task<DoctorAvailability> AddDoctorAvailableSlotAsync(DoctorAvailability doctorAvailability)
		{
			await _context.DoctorAvailabilities.AddAsync(doctorAvailability);
			await _context.SaveChangesAsync();
			return doctorAvailability;
		}
		public async Task UpdateDoctorAvailableSlotAsync(DoctorAvailability doctorAvailability)
		{
			_context.DoctorAvailabilities.Update(doctorAvailability);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteDoctorAvailableSlotAsync(DoctorAvailability doctorAvailability)
		{
			_context.DoctorAvailabilities.Remove(doctorAvailability);
			await _context.SaveChangesAsync();
		}
	}
}
