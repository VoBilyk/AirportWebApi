using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Airport.BLL.Interfaces;
using Airport.DAL.Interfaces;
using Airport.DAL.Entities;
using Airport.Shared.DTO;


namespace Airport.BLL.Services
{
    public class FlightService : IFlightService
    {
        private IUnitOfWork db;
        private IMapper mapper;
        private IValidator<Flight> validator;


        public FlightService(IUnitOfWork uow, IMapper mapper, IValidator<Flight> validator)
        {
            this.db = uow;
            this.mapper = mapper;
            this.validator = validator;
        }


        public FlightDto Get(Guid id)
        {
            return mapper.Map<Flight, FlightDto>(db.FlightRepository.Get(id));
        }

        public List<FlightDto> GetAll()
        {
            return mapper.Map<List<Flight>, List<FlightDto>>(db.FlightRepository.GetAll());
        }

        public FlightDto Create(FlightDto flightDto)
        {
            var flight = mapper.Map<FlightDto, Flight>(flightDto);
            flight.Id = Guid.NewGuid();
            flight.Tickets = db.TicketRepository.GetAll().Where(i => flightDto.TicketsId.Contains(i.Id)).ToList();

            var validationResult = validator.Validate(flight);

            if (validationResult.IsValid)
            {
                db.FlightRepository.Create(flight);
                db.SaveChanges();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
            
            return mapper.Map<Flight, FlightDto>(flight);
        }

        public FlightDto Update(Guid id, FlightDto flightDto)
        {
            var flight = mapper.Map<FlightDto, Flight>(flightDto);
            flight.Id = id;
            flight.Tickets = db.TicketRepository.GetAll().Where(i => flightDto.TicketsId.Contains(i.Id)).ToList();

            var validationResult = validator.Validate(flight);

            if (validationResult.IsValid)
            {
                db.FlightRepository.Update(flight);
                db.SaveChanges();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Flight, FlightDto>(flight);
        }

        public void Delete(Guid id)
        {
            db.FlightRepository.Delete(id);
            db.SaveChanges();
        }

        public void DeleteAll()
        {
            db.FlightRepository.Delete();
            db.SaveChanges();
        }
    }
}