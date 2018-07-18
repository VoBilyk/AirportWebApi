using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Airport.Shared.DTO
{
    public class FlightDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DeparturePoint { get; set; }

        [Required]
        public string Destinition { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public List<Guid> TicketsId { get; set; }
    }
}
