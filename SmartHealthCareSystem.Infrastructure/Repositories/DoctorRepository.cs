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
		public async Task<Doctor> AddDoctorAsync(Doctor doctor) { await _context.Doctors.AddAsync(doctor); await _context.SaveChangesAsync(); return doctor; }
		public async Task UpdateDoctorAsync(Doctor doctor) { _context.Doctors.Update(doctor); await _context.SaveChangesAsync(); }
		public async Task DeleteDoctorAsync(int id)
		{
			var doctor = await _context.Doctors.FindAsync(id);
			if (doctor != null) _context.Doctors.Remove(doctor);
			await _context.SaveChangesAsync();
		}
	}
}
