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

		public async Task<Appointment> GetAppointmentByIdAsync(int id) => await _context.Appointments.FindAsync(id);
		public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(int? appointmentId,int? doctorId, int? patientId,DateTime? fromDateTime, DateTime? toDateTime,string? status)
		{
			return await _context.Appointments.Where(a =>
				(a.FK_DoctorId == doctorId || doctorId == null || doctorId == 0) 
				&& (a.FK_PatientId == patientId || patientId == null || patientId == 0)
				&& (a.AppointmentDateTime >= fromDateTime || fromDateTime == null)
				&& (a.AppointmentDateTime <= toDateTime || toDateTime == null)
				&& (a.Status.Equals(status) || string.IsNullOrWhiteSpace(status))
				&& (a.Id == appointmentId || appointmentId == null || appointmentId == 0)
			).ToListAsync();
		}
		public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
		{
			await _context.Appointments.AddAsync(appointment);
			await _context.SaveChangesAsync();
			return appointment;
		}
		public async Task UpdateAppointmentAsync(Appointment appointment)
		{
			_context.Appointments.Update(appointment);
			await _context.SaveChangesAsync();
		}
		public async Task<bool> IsPatientAndDoctorAppointmentWithSameDateAsync(int patientId, int doctorId, DateTime date)
		{
			return await _context.Appointments.AnyAsync(u => u.FK_PatientId == patientId && u.FK_DoctorId==doctorId && u.AppointmentDateTime.Date == date.Date);
		}
	}
}
