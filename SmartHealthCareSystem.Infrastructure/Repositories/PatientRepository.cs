using Microsoft.EntityFrameworkCore;
using SmartHealthCareSystem.Domain.Entities;
using SmartHealthCareSystem.Domain.Interfaces;
using SmartHealthCareSystem.Infrastructure.Data;

namespace SmartHealthCareSystem.Infrastructure.Repositories;

public class PatientRepository :IPatientRepository
{
    private readonly AppDbContext _context;

    public PatientRepository(AppDbContext context)
    {
        _context = context;
    }

	public async Task<Patient> GetPatientByIdAsync(int id) => await _context.Patients.FindAsync(id);
	public async Task<bool> IsPatientWithEmailAsync(string email)
	{
		return await _context.Patients.AnyAsync(u => u.ContactEMail == email);
	}
	public async Task<IEnumerable<Patient>> GetAllPatientsAsync() => await _context.Patients.ToListAsync();
	public async Task<Patient> AddPatientAsync(Patient patient) 
	{
		await _context.Patients.AddAsync(patient); 
		await _context.SaveChangesAsync(); 
		return patient; 
	}
	public async Task UpdatePatientAsync(Patient patient)
	{
		_context.Patients.Update(patient); 
		await _context.SaveChangesAsync();
	}
	public async Task DeletePatientAsync(Patient patient)
	{
		 _context.Patients.Remove(patient);
		await _context.SaveChangesAsync();
	}
}
