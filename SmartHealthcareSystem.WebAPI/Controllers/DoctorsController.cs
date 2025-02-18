using Microsoft.AspNetCore.Mvc;
using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Application.Interfaces;
using SmartHealthCareSystem.Application.Services;
using SmartHealthCareSystem.Domain.Entities;
using SmartHealthCareSystem.Infrastructure.Utilities;

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

		[HttpGet]
		public async Task<IActionResult> GetAllDoctors() => Ok(await _doctorService.GetAllDoctorsAsync());

		[HttpGet("{id}")]
		public async Task<IActionResult> GetDoctorById(int id) => Ok(await _doctorService.GetDoctorByIdAsync(id));


		[HttpPost]
		public async Task<IActionResult> AddDoctor([FromBody] DoctorInsertModel doctorModel)
		{
			if (await _doctorService.IsDoctorWithEmailAsync(doctorModel.ContactEMail))
			{
				return BadRequest("Email already exists.");
			}
			var hashedPassword = PasswordHasherHelper.HashPassword(doctorModel.Password);
			var doctor = await _doctorService.AddDoctorAsync(doctorModel, hashedPassword);
			return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, doctor);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateDoctor(int id, [FromBody] Doctor doctor)
		{
			if (id != doctor.Id) return BadRequest();
			await _doctorService.UpdateDoctorAsync(doctor);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDoctor(int id)
		{
			await _doctorService.DeleteDoctorAsync(id);
			return NoContent();
		}



	}
}
