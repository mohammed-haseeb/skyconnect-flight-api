using AirlineReservation.Models;
using AirlineReservation.Models.Dto;
using AutoMapper;

namespace AirlineReservation
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Flight, FlightDTO>().ReverseMap();
            CreateMap<Flight, FlightAddDTO>().ReverseMap();
            CreateMap<Flight, FlightUpdateDTO>().ReverseMap();
        }
    }
}
