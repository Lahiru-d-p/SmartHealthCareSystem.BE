using Microsoft.AspNetCore.Mvc;
using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Application.Interfaces;
using SmartHealthCareSystem.Infrastructure.Data;
using SmartHealthCareSystem.Infrastructure.Utilities;

namespace SmartHealthcareSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
	{

		private readonly AppDbContext _context;
		private readonly IAuthService _authService;
		private readonly IUserService _userService;
		private readonly IConfiguration _configuration;
		public AuthController(AppDbContext context,IAuthService authService,IUserService userService,IConfiguration configuration)
		{
			_context = context;
			_authService = authService;
			_userService = userService;
			_configuration = configuration;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestModel loginRequest)
		{
			var user = await _userService.GetUserByEmailAsync(loginRequest.Email);
			if (user == null) return Unauthorized("User not found.");

			if (!PasswordHasherHelper.VerifyPassword(user.PasswordHash, loginRequest.Password))
				return Unauthorized("Invalid password.");

			var token = JwtHelper.GenerateToken(user.ContactEMail, user.Role, _configuration);

			return Ok(new
			{
				Token = token,
				UserId = user.Id,
				Role = user.Role
			});
		}

	}
}
