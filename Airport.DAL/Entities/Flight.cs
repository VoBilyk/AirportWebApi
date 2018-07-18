using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Airport.DAL.Interfaces;

namespace Airport.DAL.Entities
{
    public class Flight : IEntity
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DeparturePoint { get; set; }

        [Required]
        public string Destinition { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
