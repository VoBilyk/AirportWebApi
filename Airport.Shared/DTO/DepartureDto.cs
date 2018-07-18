using System;
using System.ComponentModel.DataAnnotations;


namespace Airport.Shared.DTO
{
    public class DepartureDto
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public Guid CrewId { get; set; }

        [Required]
        public Guid AirplaneId { get; set; }
    }
}
