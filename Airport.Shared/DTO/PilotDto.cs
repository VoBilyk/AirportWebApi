using System;
using System.ComponentModel.DataAnnotations;


namespace Airport.Shared.DTO
{
    public class PilotDto
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string SecondName { get; set; }

        [Required]
        public int Experience { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
