using Microsoft.AspNetCore.Mvc;
using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Application.Interfaces;
using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthcareSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
		private readonly IDoctorService _doctorService;

		public DoctorsController(IDoctorService doctorService)
		{
			_doctorService = doctorService;
		}

		//Get All Doctors Details
		[HttpGet]
		public async Task<IActionResult> GetAllDoctors()
		{
			var doctors = await _doctorService.GetAllDoctorsAsync();
			return Ok(new ResponseModel<List<DoctorViewModel>>(true, "Doctors retrieved successfully.", doctors));
		}

		//Get All Doctors List
		[HttpGet("doctors_list")]
		public async Task<IActionResult> GetAllDoctorsList()
		{
			var doctorsList = await _doctorService.GetAllDoctorsListAsync();
			return Ok(new ResponseModel<List<UserListModel>>(true, "Doctors List retrieved successfully.", doctorsList));
		}

		//Get Doctor By Id
		[HttpGet("{id}")]
		public async Task<IActionResult> GetDoctorById(int id)
		{
			var doctor = await _doctorService.GetDoctorByIdAsync(id);
			return Ok(new ResponseModel<DoctorViewModel>(true, "Doctor retrieved successfully.", doctor));
		}

		//Add Doctor
		[HttpPost]
		public async Task<IActionResult> AddDoctor([FromBody] DoctorInsertModel doctorModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new ResponseModel<object>(false, "Invalid request.", ModelState.Values.SelectMany(v => v.Errors)));
			}

			var InsertedDoctor = await _doctorService.AddDoctorAsync(doctorModel);

			return Ok(new ResponseModel<DoctorViewModel>(true, "Doctor Inserted successfully.", InsertedDoctor));
		}

		//Update Doctor
		[HttpPut]
		public async Task<IActionResult> UpdateDoctor([FromBody] DoctorUpdateModel doctorModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new ResponseModel<object>(false, "Invalid request.", ModelState.Values.SelectMany(v => v.Errors)));
			}

			var updatedDoctor = await _doctorService.UpdateDoctorAsync(doctorModel);

			return Ok(new ResponseModel<DoctorViewModel>(true, "Doctor updated successfully.", updatedDoctor));
		}

		//Delete Doctor
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDoctor(int id)
		{
			await _doctorService.DeleteDoctorAsync(id);

			return Ok(new ResponseModel<object>(true, "Doctor deleted successfully.", null));
		}
		
	}
}
