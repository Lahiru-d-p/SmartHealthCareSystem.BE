using Microsoft.EntityFrameworkCore;
using SmartHealthCareSystem.Domain.Entities;
using SmartHealthCareSystem.Domain.Interfaces;
using SmartHealthCareSystem.Infrastructure.Data;

namespace SmartHealthCareSystem.Infrastructure.Repositories
{
	public class AppointmentRepository : IAppointmentRepository
	{
		private readonly AppDbContext _context;

		public AppointmentRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Appointment appointment)
		{
			await _context.Appointments.AddAsync(appointment);
			await _context.SaveChangesAsync();
		}

		public async Task<List<Appointment>> GetAppointmentsForDoctorAsync(int doctorId)
		{
			return await _context.Appointments.Where(a => a.FK_DoctorId == doctorId).ToListAsync();
		}

		public async Task<List<Appointment>> GetAppointmentsForPatientAsync(int patientId)
		{
			return await _context.Appointments.Where(a => a.FK_PatientId == patientId).ToListAsync();
		}
	}
}
