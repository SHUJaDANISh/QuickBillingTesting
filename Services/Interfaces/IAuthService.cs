using QuickBiilingTesting.Models.Dto;
using QuickBiilingTesting.Models.Responses;

namespace QuickBiilingTesting.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginDto loginDto);
        Task<int> Register(RegisterDto registerDto);
    }
}
