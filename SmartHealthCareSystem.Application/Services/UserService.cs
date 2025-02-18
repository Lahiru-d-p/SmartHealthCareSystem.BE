using SmartHealthCareSystem.Application.Interfaces;
using SmartHealthCareSystem.Domain.Entities;
using SmartHealthCareSystem.Domain.Interfaces;

namespace SmartHealthCareSystem.Application.Services
{
    public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<User> GetUserByEmailAsync(string email) => await _userRepository.GetUserByEmailAsync(email);

	}
}
