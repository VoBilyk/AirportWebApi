using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Airport.Shared.DTO
{
    public class CrewDto
    {
        public Guid Id { get; set; }

        [Required]
        public Guid PilotId { get; set; }

        [Required]
        public List<Guid> StewardessesId { get; set; }
    }
}
