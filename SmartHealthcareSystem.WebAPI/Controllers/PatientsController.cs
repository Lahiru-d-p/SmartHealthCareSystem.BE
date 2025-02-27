using Microsoft.AspNetCore.Mvc;
using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Application.Interfaces;
using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthcareSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
		private readonly IPatientService _patientService;

		public PatientsController(IPatientService patientService)
		{
			_patientService = patientService;
		}

		//Get All Patients Details
		[HttpGet]
		public async Task<IActionResult> GetAllPatients()
		{
			var patients = await _patientService.GetAllPatientsAsync();
			return Ok(new ResponseModel<List<Patient>>(true, "Patients retrieved successfully.", patients));
		}

		//Get All Patients List
		[HttpGet]
		public async Task<IActionResult> GetAllPatientsList()
		{
			var patientsList = await _patientService.GetAllPatientsListAsync();
			return Ok(new ResponseModel<List<UserListModel>>(true, "Patients List retrieved successfully.", patientsList));
		}

		//Get Patient By Id
		[HttpGet("{id}")]
		public async Task<IActionResult> GetPatientById(int id)
		{
			var patient = await _patientService.GetPatientByIdAsync(id);
			return Ok(new ResponseModel<Patient>(true, "Patient  retrieved successfully.", patient));			
		}

		//Add Patient
		[HttpPost]
		public async Task<IActionResult> AddPatient([FromBody] PatientInsertModel patientModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new ResponseModel<object>(false, "Invalid request.", ModelState.Values.SelectMany(v => v.Errors)));
			}

			var InsertedPatient = await _patientService.AddPatientAsync(patientModel);

			return Ok(new ResponseModel<Patient>(true, "Patient Inserted successfully.", InsertedPatient));
		}

		//Update Patient
		[HttpPut]
		public async Task<IActionResult> UpdatePatient([FromBody] PatientUpdateModel patientModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new ResponseModel<object>(false, "Invalid request.", ModelState.Values.SelectMany(v => v.Errors)));
			}

			var updatedPatient = await _patientService.UpdatePatientAsync(patientModel);

			return Ok(new ResponseModel<Patient>(true, "Patient updated successfully.", updatedPatient));
		}

		//Delete Patient
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePatient(int id)
		{
			await _patientService.DeletePatientAsync(id);

			return Ok(new ResponseModel<object>(true, "Patient deleted successfully.", null));
		}
	}
}
