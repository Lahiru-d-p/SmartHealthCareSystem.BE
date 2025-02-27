using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Application.Interfaces;

namespace SmartHealthcareSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {

		private readonly IAppointmentService _appointmentService;

		public AppointmentsController(IAppointmentService appointmentService)
		{
			_appointmentService = appointmentService;
		}



	}
}
