using AirlineReservation.Models;
using AirlineReservation.Repository.IRepository;
using AirlineReservation.Services.IServices;

namespace AirlineReservation.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task AddFlightAsync(Flight flight)
        {
            await _flightRepository.AddFlightAsync(flight);
        }

        public Task DeleteFlightAsync(int flightId)
        {
            return _flightRepository.DeleteFlightAsync(flightId);
        }

        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await _flightRepository.GetAllFlightsAsync();
        }

        public async Task<Flight> GetFlightByIdAsync(int flightId)
        {
            return await _flightRepository.GetFlightByIdAsync(flightId);
        }

        public async Task<Flight> GetFlightByNumberAsync(string flightNumber)
        {
            return await _flightRepository.GetFlightByNumberAsync(flightNumber);
        }

        public Task UpdateFlightAsync(Flight flight)
        {
            return _flightRepository.UpdateFlightAsync(flight);
        }
    }
}
