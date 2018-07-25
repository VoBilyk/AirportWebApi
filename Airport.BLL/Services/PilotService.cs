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
    public class PilotService : IPilotService
    {
        private IUnitOfWork db;
        private IMapper mapper;
        private IValidator<Pilot> validator;
        
        public PilotService(IUnitOfWork uow, IMapper mapper, IValidator<Pilot> validator)
        {
            this.db = uow;
            this.mapper = mapper;
            this.validator = validator;
        }


        public async Task<PilotDto> GetAsync(Guid id)
        {
            Pilot pilot = await db.PilotRepositiry.GetAsync(id);
            return mapper.Map<Pilot, PilotDto>(pilot);
        }

        public async Task<List<PilotDto>> GetAllAsync()
        {
            var pilots = await db.PilotRepositiry.GetAllAsync();
            return mapper.Map<List<Pilot>, List<PilotDto>>(pilots);
        }
        
        public async Task<PilotDto> CreateAsync(PilotDto pilotDto)
        {
            var pilot = mapper.Map<PilotDto, Pilot>(pilotDto);
            pilot.Id = Guid.NewGuid();

            var validationResult = validator.Validate(pilot);

            if (validationResult.IsValid)
            {
                await db.PilotRepositiry.CreateAsync(pilot);
                await db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Pilot, PilotDto>(pilot);
        }

        public async Task<PilotDto> UpdateAsync(Guid id, PilotDto pilotDto)
        {
            var pilot = mapper.Map<PilotDto, Pilot>(pilotDto);
            pilot.Id = id;

            var validationResult = validator.Validate(pilot);

            if (validationResult.IsValid)
            {
                await db.PilotRepositiry.UpdateAsync(pilot);
                await db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Pilot, PilotDto>(pilot);
        }

        public async Task DeleteAsync(Guid id)
        {
            await db.PilotRepositiry.DeleteAsync(id);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            await db.PilotRepositiry.DeleteAsync();
            await db.SaveChangesAsync();
        }
    }
}