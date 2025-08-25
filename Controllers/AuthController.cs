using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using QuickBiilingTesting.Models.Dto;
using QuickBiilingTesting.Models.Responses;
using QuickBiilingTesting.Services.Interfaces;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuickBiilingTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly IValidator<RegisterDto> _validator;

        public AuthController(IAuthService authService, IValidator<RegisterDto> validator, ILogger<AuthController> logger)
        {
            _authService = authService;
            _validator = validator;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginDto loginDto)
        {
            var authResponse = await _authService.Login(loginDto);
            if (authResponse == null)
                return Unauthorized("Invalid username or password");
            return Ok(authResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(registerDto);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                _logger.LogError("{ErrorMessage}.\nErrors:\n {Errors}\nRequest Data: {RequestJson}",
                    "Validation Error",
                    string.Join(",\n ", validationErrors),
                    JsonSerializer.Serialize(registerDto));

                return BadRequest(new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Success = false,
                    Message = "Validation Error",
                    Errors = validationErrors
                });
            }

            try
            {
                var userId = await _authService.Register(registerDto);

                return Ok(new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = true,
                    Message = "User Added Successfully",
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user.");

                return BadRequest(new ApiResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Success = false,
                    Message = ex.Message,
                    Errors = new List<string> { ex.Message }
                });
            }
            //public async Task<ActionResult<int>> Register([FromBody] RegisterDto registerDto)
            //{
            //    try
            //    {
            //        var userId = await _authService.Register(registerDto);
            //        return Ok(userId);
            //    }
            //    catch (Exception ex)
            //    {
            //        return BadRequest(ex.Message);
            //    }
            //}
        }
    }}
