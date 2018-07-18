using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace Airport.Shared.DTO
{
    public class PilotDto
    {
        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        [Required]
        public int Experience { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public List<string> CrewsId { get; set; }
    }
}
