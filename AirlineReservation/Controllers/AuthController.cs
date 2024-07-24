using AirlineReservation.Models.Dto;
using AirlineReservation.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var isValidUser = await _authService.LoginAsync(loginDto);
            if (isValidUser)
            {
                var tokenString = _authService.GenerateTokenString(loginDto);
                return Ok(tokenString);
            }

            return Unauthorized("Invalid Credentials");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            if (result is true)
            {
                return Ok("Registered Successfully.");
            }

            return BadRequest();
        }
    }
}
