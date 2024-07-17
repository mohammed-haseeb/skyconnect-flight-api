using AirlineReservation.Models;

namespace AirlineReservation.Repository.IRepository
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllFlightsAsync();
        Task<Flight> GetFlight(int flightId);
        Task AddFlightAsync(Flight flight);
        Task UpdateFlightAsync(Flight flight);
        Task DeleteFlightAsync(int flightId);
    }
}
