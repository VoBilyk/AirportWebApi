using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Airport.DAL.Interfaces;

namespace Airport.DAL.Entities
{
    public class Aeroplane : IEntity
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Aeroplane name must be not less than 3 symbols")]
        public string Name { get; set; }

        [Required]
        public AeroplaneType AeroplaneType { get; set; }

        [Required]
        public double LifeTimeHourses { get; set; }

        public virtual ICollection<Departure> Departures { get; set; }

        [NotMapped]
        private TimeSpan LifetimeTimeSpan;

        [NotMapped]
        public TimeSpan LifetimeFullForm {
            get
            {
                return LifetimeTimeSpan;
            }

            set
            {
                LifetimeTimeSpan = value;
                LifeTimeHourses = value.TotalHours;
            }
        }
    }
}
