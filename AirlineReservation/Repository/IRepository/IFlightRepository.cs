using AirlineReservation.Models;

namespace AirlineReservation.Repository.IRepository
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllFlightsAsync();
        Task<Flight> GetFlightByIdAsync(int flightId);
        Task<Flight> GetFlightByNumberAsync(string flightNumber);
        Task AddFlightAsync(Flight flight);
        Task UpdateFlightAsync(Flight flight);
        Task DeleteFlightAsync(int flightId);
    }
}
