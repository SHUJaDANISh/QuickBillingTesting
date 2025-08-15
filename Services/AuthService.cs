using QuickBiilingTesting.Data.Repository;
using QuickBiilingTesting.Models.Dto;
using QuickBiilingTesting.Models.Entities;
using QuickBiilingTesting.Models.Responses;
using QuickBiilingTesting.Services.Interfaces;
using QuickBiilingTesting.Utilities;


namespace QuickBiilingTesting.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtHandler _jwtHandler;

        public AuthService(IUserRepository userRepository, JwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
        }

        public async Task<AuthResponse> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsername(loginDto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password)) // Ensure correct namespace usage
            {
                return null;
            }

            var token = _jwtHandler.GenerateToken(user);
            return new AuthResponse { Token = token, Expiration = DateTime.UtcNow.AddMinutes(30) };
        }

        public async Task<int> Register(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetUserByUsername(registerDto.Username);
            if (existingUser != null)
            {
                throw new Exception("Username already exists");
            }

            var user = new User
            {
                Username = registerDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password), // Ensure correct namespace usage
                Email = registerDto.Email,
                Role = "User" // Default role
            };

            return await _userRepository.RegisterUser(user);
        }
    }
}
