using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineReservation.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        [ForeignKey("FlightId")]
        public Flight Flight { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public DateTime ReservationTime { get; set; }
    }
}
