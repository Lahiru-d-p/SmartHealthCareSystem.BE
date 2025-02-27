using SmartHealthCareSystem.Application.DTOs;
using SmartHealthCareSystem.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using SmartHealthCareSystem.Domain.Interfaces;
using SmartHealthCareSystem.Common.Utilities;

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

		public async Task<LoginResponseModel> LoginAsync(LoginRequestModel loginRequest)
		{
			try
			{
				var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);
				if (user == null)
				{
					throw new KeyNotFoundException("User not found.");
				}

				if (!PasswordHasherHelper.VerifyPassword(user.PasswordHash, loginRequest.Password))
				{
					throw new UnauthorizedAccessException("Invalid password.");
				}

				var token = JwtHelper.GenerateToken(user.ContactEMail, user.Role, _configuration);

				return new LoginResponseModel
				{
					Token = token,
					UserId = user.Id,
					Role = user.Role
				};
			}
			catch(Exception ex)
			{
				throw new InvalidOperationException("User Login failed.", ex);
			}

		}
	}
}
