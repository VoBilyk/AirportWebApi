using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace Airport.Shared.DTO
{
    public class FullCrewDto
    {
        public string Id { get; set; }

        [Required]
        public List<PilotDto> Pilot { get; set; }

        [Required]
        public List<StewardessDto> Stewardess { get; set; }
    }
}
