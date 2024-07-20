using AirlineReservation.Data;
using AirlineReservation.Models;
using AirlineReservation.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AirlineContext _airlineDb;

        public FlightRepository(AirlineContext airlineDb)
        {
            _airlineDb = airlineDb;
        }

        public async Task AddFlightAsync(Flight flight)
        {
            await _airlineDb.Flights.AddAsync(flight);
            await SaveAsync();
        }

        public async Task DeleteFlightAsync(int flightId)
        {
            var flight = await _airlineDb.Flights.FindAsync(flightId);

            if (flight is not null)
            {
                _airlineDb.Flights.Remove(flight);
                await SaveAsync();
            }
        }

        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await _airlineDb.Flights.ToListAsync();
        }

        public async Task<Flight> GetFlightByIdAsync(int flightId)
        {
            return await _airlineDb.Flights.FirstOrDefaultAsync(u => u.Id == flightId);
        }

        public async Task UpdateFlightAsync(Flight flight)
        {
            _airlineDb.Flights.Update(flight);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _airlineDb.SaveChangesAsync();
        }

        public async Task<Flight> GetFlightByNumberAsync(string flightNumber)
        {
            return await _airlineDb.Flights.FirstOrDefaultAsync(u => u.FlightNumber == flightNumber);
        }
    }
}
