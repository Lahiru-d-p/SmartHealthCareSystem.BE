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


		//Get Doctor all Available Time Slotes By Doctor Id
		[HttpGet("all_available_time/{id}")]
		public async Task<IActionResult> GetAllAvailableTimeSlotesByDoctorIdAsync(int id)
		{
			var result = await _doctorService.GetAllAvailableTimeSlotesByDoctorIdAsync(id);
			return Ok(new ResponseModel<List<AllAvailableSlotesViewModel>>(true, "Time Slotes retrieved successfully.", result));
		}

		//Add Doctor Available Time Slote
		[HttpPost]
		public async Task<IActionResult> AddDoctorAvailableTimeSlote([FromBody] DoctorTimeSloteInsertModel doctorModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new ResponseModel<object>(false, "Invalid request.", ModelState.Values.SelectMany(v => v.Errors)));
			}

			var insertedDoctorTimeSlote = await _doctorService.AddAvailableTimeSloteAsync(doctorModel);

			return Ok(new ResponseModel<AllAvailableSlotesViewModel>(true, "Time Slote Inserted successfully.", insertedDoctorTimeSlote));
		}

		//Update Doctor Available Time Slote
		[HttpPut]
		public async Task<IActionResult> UpdateDoctorAvailableTimeSlote([FromBody] DoctorTimeSloteUpdateModel doctorModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new ResponseModel<object>(false, "Invalid request.", ModelState.Values.SelectMany(v => v.Errors)));
			}

			var updatedDoctorTimeSlote = await _doctorService.UpdateAvailableTimeSloteAsync(doctorModel);

			return Ok(new ResponseModel<AllAvailableSlotesViewModel>(true, "Time Slote updated successfully.", updatedDoctorTimeSlote));
		}

		//Delete Doctor Available Time Slote
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDoctorAvailableTimeSlote(int id)
		{
			await _doctorService.DeleteAvailableTimeSloteAsync(id);

			return Ok(new ResponseModel<object>(true, "Time Slote deleted successfully.", null));
		}

		//Get Doctor Available Time Slotes list By Doctor Id
		//[HttpGet("available_time_list/{id}")]
		//public async Task<IActionResult> GetAppointmentTimeSlotesListByDoctorIdAsync(int id)
		//{
		//	var result = await _doctorService.GetAppointmentTimeSlotesListByDoctorIdAsync(id);
		//	return Ok(new ResponseModel<AvailableSlotesViewModel>(true, "Time list retrieved successfully.", result));
		//}
	}
}
