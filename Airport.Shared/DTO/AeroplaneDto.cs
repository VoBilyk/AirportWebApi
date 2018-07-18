using System;
using System.ComponentModel.DataAnnotations;

namespace Airport.Shared.DTO
{
    public class AeroplaneDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid AeroplaneTypeId { get; set; }

        [Required]
        public TimeSpan Lifetime { get; set; }
    }
}
