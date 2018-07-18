using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<FlightDto>> GetAllAsync()
        {
            var flights = db.FlightRepository.GetAllAsync();
            return await mapper.Map<Task<List<Flight>>, Task<List<FlightDto>>>(flights);
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
                db.SaveChangesAsync();
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
                db.SaveChangesAsync();
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
            db.SaveChangesAsync();
        }

        public void DeleteAll()
        {
            db.FlightRepository.Delete();
            db.SaveChangesAsync();
        }
    }
}