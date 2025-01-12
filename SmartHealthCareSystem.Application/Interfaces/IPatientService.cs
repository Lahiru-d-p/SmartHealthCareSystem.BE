using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthCareSystem.Application.Interfaces;

public interface IPatientService
{
    Task<Patient> GetPatientByIDAsync(int id);
}
