using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthCareSystem.Application.Interfaces
{
    public interface IUserService
	{
		Task<User> GetUserByEmailAsync(string email);
	}
}
