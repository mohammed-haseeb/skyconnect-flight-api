using AirlineReservation.Models;
using AirlineReservation.Models.Dto;
using AirlineReservation.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Controllers
{
    [Route("api/flights")]
    [ApiController]
    [Authorize]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly ILogger<FlightsController> _logger;

        public FlightsController(IFlightService flightService, IMapper mapper, ILogger<FlightsController> logger)
        {
            _flightService = flightService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FlightDTO>>> GetAllFlights()
        {
            try
            {
                IEnumerable<Flight> flightList = await _flightService.GetAllFlightsAsync();
                return Ok(_mapper.Map<IEnumerable<FlightDTO>>(flightList));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while retrieving you flights.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("{id}", Name = "GetFlightById")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FlightDTO>> GetFlightById(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Error: Invalid Id.");
                return BadRequest();
            }

            try
            {
                var flight = await _flightService.GetFlightByIdAsync(id);

                if (flight is null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<FlightDTO>(flight));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured while retrieving flight with Id:{id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FlightDTO>> AddFlight([FromBody] FlightAddDTO flightDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                if (flightDto is null)
                {
                    return BadRequest();
                }

                var existFlight = await _flightService.GetFlightByNumberAsync(flightDto.FlightNumber);

                if (existFlight is not null)
                {
                    return Conflict(new { message = "Flight with same number already exists." });
                }

                Flight flight = _mapper.Map<Flight>(flightDto);

                await _flightService.AddFlightAsync(flight);

                return CreatedAtRoute("GetFlightById", new { id = flight.Id }, flight);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while adding flight.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var flight = _flightService.GetFlightByIdAsync(id);

                if (flight is null)
                {
                    return NotFound();
                }

                await _flightService.DeleteFlightAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured while deleting flight with Id:{id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateFlight(int id, [FromBody] FlightUpdateDTO updatedFlight)
        {
            if (id <= 0 || id != updatedFlight.Id)
            {
                return BadRequest();
            }

            if (updatedFlight == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var flight = await _flightService.GetFlightByIdAsync(id);

                if (flight is null)
                {
                    return NotFound();
                }

                _mapper.Map(updatedFlight, flight);

                await _flightService.UpdateFlightAsync(flight);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured while updating flight with Id:{id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
