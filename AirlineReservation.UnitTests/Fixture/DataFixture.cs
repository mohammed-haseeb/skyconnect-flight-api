using AirlineReservation.Models;
using AirlineReservation.Models.Dto;
using AirlineReservation.Models.Enum;

namespace AirlineReservation.UnitTests.Fixture
{
    public class DataFixture
    {
        public static IEnumerable<Flight> GetAllFlights()
        {
            return new List<Flight>
            {
                new Flight
                {
                    Id = 1,
                    FlightNumber = "QR579",
                    Destination = "DOH",
                    Origin = "BLR",
                    DepartureTime = DateTime.UtcNow,
                    Status = FlightStatus.Scheduled
                },
                new Flight
                {
                    Id = 2,
                    FlightNumber = "EK405",
                    Destination = "DXB",
                    Origin = "SIN",
                    DepartureTime = DateTime.UtcNow,
                    Status = FlightStatus.Departed
                },
                new Flight
                {
                    Id = 3,
                    FlightNumber = "AA101",
                    Destination = "JFK",
                    Origin = "LAX",
                    DepartureTime = DateTime.UtcNow,
                    Status = FlightStatus.Scheduled
                }
            };
        }

        public static List<FlightDTO> GetAllFlightDtos()
        {
            return new List<FlightDTO>
            {
                new FlightDTO
                {
                    Id = 1,
                    FlightNumber = "QR579",
                    Destination = "DOH",
                    Origin = "BLR",
                    DepartureTime = DateTime.UtcNow.AddHours(2),
                    Status = FlightStatus.Scheduled
                },
                new FlightDTO
                {
                    Id = 2,
                    FlightNumber = "EK405",
                    Destination = "DXB",
                    Origin = "SIN",
                    DepartureTime = DateTime.UtcNow.AddHours(4),
                    Status =FlightStatus.Departed
                },
                new FlightDTO
                {
                    Id = 3,
                    FlightNumber = "AA101",
                    Destination = "JFK",
                    Origin = "LAX",
                    DepartureTime = DateTime.UtcNow.AddHours(6),
                    Status = FlightStatus.Canceled

                }
            };
        }
    }
}
