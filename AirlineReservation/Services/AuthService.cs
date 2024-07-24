using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AirlineReservation.Models;
using AirlineReservation.Models.Dto;
using AirlineReservation.Repository.IRepository;
using AirlineReservation.Services.IServices;
using Microsoft.IdentityModel.Tokens;

namespace AirlineReservation.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public string GenerateTokenString(LoginDTO loginDto)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginDto.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

            SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }

        public async Task<bool> LoginAsync(LoginDTO loginDto)
        {
            var user = await _userRepository.GetUserByNameAsync(loginDto.UserName);
            if (user is null)
            {
                throw new Exception("User not found");
            }

            var result = await _userRepository.CheckPasswordAsync(user, loginDto.Password);

            if (result is not true)
            {
                throw new Exception("Invalid Password");
            }

            return true;
        }

        public async Task<bool> RegisterAsync(RegisterDTO registerDto)
        {
            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,

            };

            return await _userRepository.RegisterUserAsync(user, registerDto.Password);
        }
    }
}
