using System;
using System.ComponentModel.DataAnnotations;
using Airport.DAL.Interfaces;

namespace Airport.DAL.Entities
{
    public class Ticket : IEntity
    {
        public Guid Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Guid FlightId { get; set; }

        public Flight Flight { get; set; }
    }
}
