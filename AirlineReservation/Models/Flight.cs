using AirlineReservation.Models.Enum;

namespace AirlineReservation.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string Destination { get; set; }
        public string Origin { get; set; }
        public DateTime DepartureTime { get; set; }
        public FlightStatus Status { get; set; }
    }
}
