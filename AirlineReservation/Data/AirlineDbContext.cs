    using AirlineReservation.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    namespace AirlineReservation.Data
    {
        public class AirlineDbContext : IdentityDbContext
        {
            public AirlineDbContext(DbContextOptions<AirlineDbContext> options) : base(options)
            {

            }

            public DbSet<Flight> Flights { get; set; }
            public DbSet<User> Users { get; set; }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);
            }
        }
    }
