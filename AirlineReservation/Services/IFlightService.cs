using AirlineReservation.Models;

namespace AirlineReservation.Services
{
    public interface IFlightService
    {
        Task<IEnumerable<Flight>> GetAllFlightsAsync();
        Task<Flight> GetFlightByIdAsync(int flightId);
        Task<Flight> GetFlightByNumberAsync(string flightNumber);
        Task AddFlightAsync(Flight flight);
        Task UpdateFlightAsync(Flight flight);
        Task DeleteFlightAsync(int flightId);
    }
}
