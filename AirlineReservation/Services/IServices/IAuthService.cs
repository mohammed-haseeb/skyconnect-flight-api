using AirlineReservation.Models.Dto;

namespace AirlineReservation.Services.IServices
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginDTO logicDto);
        Task<bool> RegisterAsync(RegisterDTO registerDto);
        string GenerateTokenString(LoginDTO loginDto);
    }
}