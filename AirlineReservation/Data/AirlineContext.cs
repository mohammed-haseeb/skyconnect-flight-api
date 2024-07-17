using AirlineReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservation.Data
{
    public class AirlineContext : DbContext
    {
        public AirlineContext(DbContextOptions<AirlineContext> options) : base(options)
        {

        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
