using AirlineReservation.Data;
using AirlineReservation.Models;
using AirlineReservation.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AirlineContext _airlineDb;
        public ReservationRepository(AirlineContext airlineDb)
        {
            _airlineDb = airlineDb;
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            await _airlineDb.Reservations.AddAsync(reservation);
            await SaveAsync();
        }

        public async Task DeleteReservationAsync(int reservationId)
        {
            var reservation = await _airlineDb.Reservations.FindAsync(reservationId);
            if (reservation is not null)
            {
                _airlineDb.Reservations.Remove(reservation);
                await SaveAsync();
            }
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _airlineDb.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int reservationId)
        {
            return await _airlineDb.Reservations.FirstOrDefaultAsync(u => u.Id == reservationId);
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
            _airlineDb.Reservations.Update(reservation);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _airlineDb.SaveChangesAsync();
        }
    }
}
