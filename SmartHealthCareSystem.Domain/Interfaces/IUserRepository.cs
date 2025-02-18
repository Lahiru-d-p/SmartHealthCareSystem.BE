using SmartHealthCareSystem.Domain.Entities;

namespace SmartHealthCareSystem.Domain.Interfaces
{
    public interface IUserRepository
	{
		Task<User> GetUserByEmailAsync(string email);
	}
}
