using AirlineReservation.Models;
using AirlineReservation.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Controllers
{
    [Route("api/Flights")]
    [ApiController]
    public class FlightsAPIController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly ILogger<FlightsAPIController> _logger;

        public FlightsAPIController(IFlightService flightService, ILogger<FlightsAPIController> logger)
        {
            _flightService = flightService;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllFlights")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Flight>>> GetAllFlights()
        {
            IEnumerable<Flight> flightList = await _flightService.GetAllFlightsAsync();
            return Ok(flightList);
        }


        [HttpGet("{id}", Name = "GetFlightById")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Flight>> GetFlightById(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error: Invalid Id.");
                return BadRequest();
            }

            var flight = await _flightService.GetFlightByIdAsync(id);

            if (flight is null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        [HttpPost(Name = "AddFlight")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Flight>> AddFlight([FromBody] Flight flight)
        {
            if (flight is null)
            {
                return BadRequest();
            }

            var existFlight = await _flightService.GetFlightByNumberAsync(flight.FlightNumber);

            if (existFlight is not null)
            {
                return Conflict(new { message = "Flight with same number already exists." });
            }

            await _flightService.AddFlightAsync(flight);

            return CreatedAtRoute("GetFlightById", new { id = flight.Id }, flight);
        }

        [HttpDelete(Name = "DeleteFlight")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var flight = _flightService.GetFlightByIdAsync(id);

            if (flight is null)
            {
                return NotFound();
            }

            await _flightService.DeleteFlightAsync(id);

            return NoContent();
        }

        [HttpPut(Name = "UpdateFlight")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateFlight(int id, [FromBody] Flight updatedFlight)
        {
            if (id != updatedFlight.Id || updatedFlight == null)
            {
                return BadRequest();
            }

            var flight = await _flightService.GetFlightByIdAsync(id);

            if (flight is null)
            {
                return NotFound();
            }

            flight.FlightNumber = updatedFlight.FlightNumber;
            flight.Origin = updatedFlight.Origin;
            flight.Destination = updatedFlight.Destination;
            flight.DepartureTime = updatedFlight.DepartureTime;
            flight.Status = updatedFlight.Status;

            await _flightService.UpdateFlightAsync(flight);
            return NoContent();
        }
    }
}
 