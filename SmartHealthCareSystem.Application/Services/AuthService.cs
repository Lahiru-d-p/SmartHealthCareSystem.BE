using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using SmartHealthCareSystem.Domain.Interfaces;

namespace SmartHealthCareSystem.Application.Services
{
    public class AuthService: IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfiguration _configuration;
		public AuthService(IUserRepository userRepository,IConfiguration configuration)
		{
			_userRepository = userRepository;
			_configuration = configuration;
		}

		public async Task LoginAsync(LoginRequestModel loginRequest)
		{

			
		}
	}
}
