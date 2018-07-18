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


        public async Task<FlightDto> GetAsync(Guid id)
        {
            Flight flight = await db.FlightRepository.GetAsync(id);
            return mapper.Map<Flight, FlightDto>(flight);
        }

        public async Task<List<FlightDto>> GetAllAsync()
        {
            var flights = db.FlightRepository.GetAllAsync();
            return await mapper.Map<Task<List<Flight>>, Task<List<FlightDto>>>(flights);
        }

        public async Task<FlightDto> CreateAsync(FlightDto flightDto)
        {
            var flight = mapper.Map<FlightDto, Flight>(flightDto);
            flight.Id = Guid.NewGuid();
            flight.Tickets = await db.TicketRepository.FindAsync(i => flightDto.TicketsId.Contains(i.Id));

            var validationResult = validator.Validate(flight);

            if (validationResult.IsValid)
            {
                await db.FlightRepository.CreateAsync(flight);
                await db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
            
            return mapper.Map<Flight, FlightDto>(flight);
        }

        public async Task<FlightDto> UpdateAsync(Guid id, FlightDto flightDto)
        {
            var flight = mapper.Map<FlightDto, Flight>(flightDto);
            flight.Id = id;
            flight.Tickets = await db.TicketRepository.FindAsync(i => flightDto.TicketsId.Contains(i.Id));

            var validationResult = validator.Validate(flight);

            if (validationResult.IsValid)
            {
                await db.FlightRepository.UpdateAsync(flight);
                await db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Flight, FlightDto>(flight);
        }

        public async Task DeleteAsync(Guid id)
        {
            await db.FlightRepository.DeleteAsync(id);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            await db.FlightRepository.DeleteAsync();
            await db.SaveChangesAsync();
        }
    }
}