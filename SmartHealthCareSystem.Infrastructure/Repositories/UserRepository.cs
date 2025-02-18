using Microsoft.EntityFrameworkCore;
using SmartHealthCareSystem.Domain.Entities;
using SmartHealthCareSystem.Domain.Interfaces;
using SmartHealthCareSystem.Infrastructure.Data;

namespace SmartHealthCareSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<User> GetUserByEmailAsync(string email)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.ContactEMail == email);
		}
	}
}
