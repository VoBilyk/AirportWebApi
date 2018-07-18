using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Airport.DAL.Interfaces;

namespace Airport.DAL.Entities
{
    public class AeroplaneType : IEntity
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "AeroplaneType model name must be not less than 3 symbols")]
        public string Model { get; set; }

        [Required]
        public int Places { get; set; }

        [Required]
        public int Carrying { get; set; }

        public ICollection<Aeroplane> Aeroplanes { get; set; }
    }
}
