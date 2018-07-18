using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;

using Airport.BLL.Interfaces;
using Airport.DAL.Interfaces;
using Airport.DAL.Entities;
using Airport.Shared.DTO;


namespace Airport.BLL.Services
{
    public class TicketService : ITicketService
    {
        private IUnitOfWork db;
        private IMapper mapper;
        private IValidator<Ticket> validator;
        
        public TicketService(IUnitOfWork uow, IMapper mapper, IValidator<Ticket> validator)
        {
            this.db = uow;
            this.mapper = mapper;
            this.validator = validator;
        }


        public async Task<TicketDto> GetAsync(Guid id)
        {
            var ticket = await db.TicketRepository.GetAsync(id);
            return mapper.Map<Ticket, TicketDto>(ticket);
        }

        public async Task<List<TicketDto>> GetAllAsync()
        {
            List<Ticket> tickets = await db.TicketRepository.GetAllAsync();
            return mapper.Map<List<Ticket>, List<TicketDto>>(tickets);
        }

        public async Task<TicketDto> CreateAsync(TicketDto ticketDto)
        {
            var ticket = mapper.Map<TicketDto, Ticket>(ticketDto);
            ticket.Id = Guid.NewGuid();

            ticket.Flight = await db.FlightRepository.GetAsync(ticketDto.FlightId);

            var validationResult = validator.Validate(ticket);
            if (validationResult.IsValid)
            {
                await db.TicketRepository.CreateAsync(ticket);
                db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Ticket, TicketDto>(ticket);
        }

        public async Task<TicketDto> UpdateAsync(Guid id, TicketDto ticketDto)
        {
            var ticket = mapper.Map<TicketDto, Ticket>(ticketDto);
            ticket.Id = id;
            ticket.Flight = await db.FlightRepository.GetAsync(ticketDto.FlightId);

            var validationResult = validator.Validate(ticket);
            if (validationResult.IsValid)
            {
                await db.TicketRepository.UpdateAsync(ticket);
                db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Ticket, TicketDto>(ticket);
        }

        public async Task DeleteAsync(Guid id)
        {
            await db.TicketRepository.DeleteAsync(id);
            db.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            await db.TicketRepository.DeleteAsync();
            db.SaveChangesAsync();
        }
    }
}
