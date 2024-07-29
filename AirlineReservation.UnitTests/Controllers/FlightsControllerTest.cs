using AirlineReservation.Controllers;
using AirlineReservation.Models;
using AirlineReservation.Models.Dto;
using AirlineReservation.Services.IServices;
using AirlineReservation.UnitTests.Fixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AirlineReservation.UnitTests.Controllers
{
    public class FlightsControllerTest
    {
        private readonly Mock<IFlightService> _mockFlightService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<FlightsController>> _mockLogger;
        private readonly FlightsController _controller;

        public FlightsControllerTest()
        {
            _mockFlightService = new Mock<IFlightService>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<FlightsController>>();
            _controller = new FlightsController(_mockFlightService.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllFlights_OnSuccess_ReturnsOkResult_WithListOfFlights()
        {

            // Given
            var flights = DataFixture.GetAllFlights();
            var flightDtos = DataFixture.GetAllFlightDtos();

            _mockFlightService.Setup(s => s.GetAllFlightsAsync()).ReturnsAsync(flights);
            _mockMapper.Setup(m => m.Map<IEnumerable<FlightDTO>>(It.IsAny<IEnumerable<Flight>>())).Returns(flightDtos);

            // When
            var result = await _controller.GetAllFlights();

            // Then
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeAssignableTo<IEnumerable<FlightDTO>>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public async Task GetFlightById_WithValidId_ReturnsFlightWithOkResult(int validId)
        {
            //Given
            var expectedFlight = new Flight { Id = validId, FlightNumber = $"QR579" };
            var expectedFlightDto = new FlightDTO { Id = validId, FlightNumber = $"QR579    " };

            _mockFlightService.Setup(s => s.GetFlightByIdAsync(validId)).ReturnsAsync(expectedFlight);
            _mockMapper.Setup(m => m.Map<FlightDTO>(It.IsAny<Flight>())).Returns(expectedFlightDto);

            // When
            var result = await _controller.GetFlightById(validId);

            // Then
            result.Result.Should().BeOfType<OkObjectResult>();

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task GetFlightById_WithInvalidId_ReturnsBadRequest(int invalidId)
        {
            // When
            var result = await _controller.GetFlightById(invalidId);

            // Then
            result.Should().BeOfType<ActionResult<FlightDTO>>();
            result.Result.Should().BeOfType<BadRequestResult>();
        }
    }
}

/*
 * create an instance of contorller first
 * then, create a mock object of the services to be injected
 * we don't really actually call the method, we basically mock it(here, say some method of IFlightService)
 */