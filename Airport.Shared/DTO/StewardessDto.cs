using System;
using System.ComponentModel.DataAnnotations;


namespace Airport.Shared.DTO
{
    public class StewardessDto
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
