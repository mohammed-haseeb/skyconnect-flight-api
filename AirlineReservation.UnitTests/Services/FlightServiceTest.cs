using AirlineReservation.Models;
using AirlineReservation.Repository.IRepository;
using AirlineReservation.Services;
using AirlineReservation.UnitTests.Fixture;
using FluentAssertions;
using Moq;

namespace AirlineReservation.UnitTests.Services
{
    public class FlightServiceTest
    {
        private readonly Mock<IFlightRepository> _mockFlightRepository;
        private readonly FlightService _service;

        public FlightServiceTest()
        {
            _mockFlightRepository = new Mock<IFlightRepository>();
            _service = new FlightService(_mockFlightRepository.Object);
        }

        [Fact]
        public async Task GetAllFlightsAsync_OnSuccess_ReturnsAllFlights()
        {
            // Arrange
            var expectedFlights = DataFixture.GetAllFlights();
            _mockFlightRepository.Setup(s => s.GetAllFlightsAsync()).ReturnsAsync(expectedFlights);

            // Act
            var result = await _service.GetAllFlightsAsync();

            // Assert
            result.Should().NotBeNull().And.BeEquivalentTo(expectedFlights);
            result.Should().BeAssignableTo<IEnumerable<Flight>>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetFlightByIdAsync_WithInvalidId_ReturnsNull(int invalidId)
        {
            // Act
            var result = await _service.GetFlightByIdAsync(invalidId);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public async Task GetFlightByIdAsync_WithValidId_ReturnsFlight(int validId)
        {
            //Arrange
            var expectedFlight = new Flight { Id = validId, FlightNumber = $"QR579" };

            _mockFlightRepository.Setup(s => s.GetFlightByIdAsync(validId)).ReturnsAsync(expectedFlight);

            // Act
            var result = await _service.GetFlightByIdAsync(validId);

            //Assert
            result.Should().NotBeNull().And.BeEquivalentTo(expectedFlight);
            result.Should().BeOfType<Flight>();
        }
    }
}
