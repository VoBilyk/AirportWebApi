using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
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
            var flights = await db.FlightRepository.GetAllAsync();
            return mapper.Map<List<Flight>, List<FlightDto>>(flights);
        }

        public async Task<FlightDto> CreateAsync(FlightDto flightDto)
        {
            var flight = mapper.Map<FlightDto, Flight>(flightDto);
            flight.Id = Guid.NewGuid();
            if(flightDto.TicketsId != null)
            {
                flight.Tickets = await db.TicketRepository.FindAsync(i => flightDto.TicketsId.Contains(i.Id));
            }

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

            if (flightDto.TicketsId != null)
            {
                flight.Tickets = await db.TicketRepository.FindAsync(i => flightDto.TicketsId.Contains(i.Id));
            }

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
            await db.FlightRepository.Delete(id);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            db.FlightRepository.Delete();
            await db.SaveChangesAsync();
        }

        public async Task<List<FlightDto>> GetWithDelayAsync(int delay)
        {
            var timer = new Timer(delay);
            var tcs = new TaskCompletionSource<List<FlightDto>>();

            var flights = await db.FlightRepository.GetAllAsync();
            var flightsDto = mapper.Map<List<Flight>, List<FlightDto>>(flights);
            
            timer.Elapsed += (o, e) =>
            {
                timer.Dispose();
                tcs.SetResult(flightsDto);
            };

            timer.Start();

            return await tcs.Task;
        }
    }
}