using Microsoft.AspNetCore.Mvc;
using SmartHealthCareSystem.Application.Interfaces;

namespace SmartHealthcareSystem.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientById(int id)
    {
        var patient = await _patientService.GetPatientByIDAsync(id);
        if (patient == null)
            return NotFound();
        return Ok(patient);
    }

}

