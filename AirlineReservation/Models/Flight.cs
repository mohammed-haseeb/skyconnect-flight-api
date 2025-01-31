﻿using System.ComponentModel.DataAnnotations;
using AirlineReservation.Models.Enum;

namespace AirlineReservation.Models
{
    public class Flight
    {
        public int Id { get; set; }
        [Required]
        public string FlightNumber { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public string Origin { get; set; }
        public DateTime DepartureTime { get; set; }
        public FlightStatus Status { get; set; }
    }
}
