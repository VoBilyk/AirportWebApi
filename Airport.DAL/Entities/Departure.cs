using Airport.DAL.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Airport.DAL.Entities
{
    public class Departure : IEntity
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime Time { get; set; }


        public Crew Crew { get; set; }

        
        public Aeroplane Airplane { get; set; }
    }
}
