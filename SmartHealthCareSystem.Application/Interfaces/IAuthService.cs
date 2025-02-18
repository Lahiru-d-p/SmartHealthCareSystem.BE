using SmartHealthCareSystem.Application.DTOs;

namespace SmartHealthCareSystem.Application.Interfaces
{
    public interface IAuthService
	{
		Task LoginAsync(LoginRequestModel loginRequest);
	}
}
