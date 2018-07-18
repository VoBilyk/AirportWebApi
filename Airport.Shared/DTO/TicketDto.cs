using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Airport.Shared.DTO
{
    public class TicketDto
    {
        public Guid Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public Guid FlightId { get; set; }
    }
}
