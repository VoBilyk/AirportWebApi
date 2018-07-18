using System;
using System.ComponentModel.DataAnnotations;


namespace Airport.Shared.DTO
{
    public class AeroplaneTypeDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Places { get; set; }

        [Required]
        public int Carrying { get; set; }
    }
}
