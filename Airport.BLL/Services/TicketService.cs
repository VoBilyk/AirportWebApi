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


        public TicketDto Get(Guid id)
        {
            //var ticket = db.TicketRepository.GetAsync(id);
            //return await mapper.Map<Task<Ticket>, Task<TicketDto>>(ticket);
            return new TicketDto();
        }

        public async Task<List<TicketDto>> GetAllAsync()
        {
            var tickets = db.TicketRepository.GetAllAsync();
            return await mapper.Map<Task<List<Ticket>>, Task<List<TicketDto>>>(tickets);
        }

        public TicketDto Create(TicketDto ticketDto)
        {
            var ticket = mapper.Map<TicketDto, Ticket>(ticketDto);
            ticket.Id = Guid.NewGuid();
            ticket.Flight = db.FlightRepository.Get(ticketDto.FlightId);

            var validationResult = validator.Validate(ticket);

            if (validationResult.IsValid)
            {
                db.TicketRepository.Create(ticket);
                db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Ticket, TicketDto>(ticket);
        }

        public TicketDto Update(Guid id, TicketDto ticketDto)
        {
            var ticket = mapper.Map<TicketDto, Ticket>(ticketDto);
            ticket.Id = id;
            ticket.Flight = db.FlightRepository.Get(ticketDto.FlightId);

            var validationResult = validator.Validate(ticket);

            if (validationResult.IsValid)
            {
                db.TicketRepository.Update(ticket);
                db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Ticket, TicketDto>(ticket);
        }

        public void Delete(Guid id)
        {
            db.TicketRepository.Delete(id);
            db.SaveChangesAsync();
        }

        public void DeleteAll()
        {
            db.TicketRepository.Delete();
            db.SaveChangesAsync();
        }
    }
}
