using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Airport.DAL.Interfaces;

namespace Airport.DAL.Entities
{
    public class Crew : IEntity
    {
        public Guid Id { get; set; }

        public Guid PilotId { get; set; }

        public Pilot Pilot { get; set; }
                
        [MinLength(1, ErrorMessage = "Crew can`t have less than 1 stewardess")]
        public ICollection<Stewardess> Stewardesses { get; set; }

        public ICollection<Departure> Departures { get; set; }
    }
}
