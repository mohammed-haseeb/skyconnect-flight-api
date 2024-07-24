using AirlineReservation.Models;

namespace AirlineReservation.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByNameAsync(string name);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<bool> RegisterUserAsync(User user, string password);
    }
}
