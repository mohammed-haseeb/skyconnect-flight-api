using AirlineReservation.Models;
using AirlineReservation.Models.Dto;
using AirlineReservation.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservation.Controllers
{
    [Route("api/Flights")]
    [ApiController]
    public class FlightsAPIController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly ILogger<FlightsAPIController> _logger;

        public FlightsAPIController(IFlightService flightService, IMapper mapper, ILogger<FlightsAPIController> logger)
        {
            _flightService = flightService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllFlights")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        public async Task<ActionResult<FlightDTO>> GetFlightById(int id)
        {
            try
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

                return Ok(_mapper.Map<FlightDTO>(flight));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured while retrieving flight with Id:{id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost(Name = "AddFlight")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<FlightDTO>> AddFlight([FromBody] FlightAddDTO flightDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

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

        [HttpDelete(Name = "DeleteFlight")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured while deleting flight with Id:{id}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut(Name = "UpdateFlight")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateFlight(int id, [FromBody] FlightUpdateDTO updatedFlight)
        {
            try
            {
                if (updatedFlight == null)
                {
                    return BadRequest("Data to update f");
                }

                if (id != updatedFlight.Id)
                {
                    return BadRequest();
                }


                var flight = await _flightService.GetFlightByIdAsync(id);

                if (flight is null)
                {
                    return NotFound();
                }

                //flight.FlightNumber = updatedFlight.FlightNumber;
                //flight.Origin = updatedFlight.Origin;
                //flight.Destination = updatedFlight.Destination;
                //flight.DepartureTime = updatedFlight.DepartureTime;
                //flight.Status = updatedFlight.Status;

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
