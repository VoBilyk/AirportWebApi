using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace Airport.Shared.DTO
{
    public class StewardessDto
    {
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public string CrewId { get; set; }
    }
}
