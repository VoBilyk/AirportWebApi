using System;
using System.Linq;
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
    public class CrewService : ICrewService
    {
        private IUnitOfWork db;
        private IMapper mapper;
        IValidator<Crew> validator;

        public CrewService(IUnitOfWork uow, IMapper mapper, IValidator<Crew> validator)
        {
            this.db = uow;
            this.mapper = mapper;
            this.validator = validator;
        }


        public async Task<CrewDto> GetAsync(Guid id)
        {
            Crew item = await db.CrewRepositiry.GetAsync(id);
            return mapper.Map<Crew, CrewDto>(item);
        }

        public async Task<List<CrewDto>> GetAllAsync()
        {
            List<Crew> crews = await db.CrewRepositiry.GetAllAsync();
            return mapper.Map<List<Crew>, List<CrewDto>>(crews);
        }

        public async Task<CrewDto> CreateAsync(CrewDto crewDto)
        {
            var crew = mapper.Map<CrewDto, Crew>(crewDto);

            crew.Id = Guid.NewGuid();
            crew.Pilot = await db.PilotRepositiry.GetAsync(crewDto.PilotId);
            crew.Stewardesses = await db.StewardessRepositiry.FindAsync(i => crewDto.StewardessesId.Contains(i.Id));

            var validationResult = validator.Validate(crew);

            if (validationResult.IsValid)
            {
                await db.CrewRepositiry.CreateAsync(crew);
                await db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Crew, CrewDto>(crew);
        }

        public async Task<CrewDto> UpdateAsync(Guid id, CrewDto crewDto)
        {
            var crew = mapper.Map<CrewDto, Crew>(crewDto);

            crew.Id = id;
            crew.Pilot = await db.PilotRepositiry.GetAsync(crewDto.PilotId);
            crew.Stewardesses = await db.StewardessRepositiry.FindAsync(i => crewDto.StewardessesId.Contains(i.Id));

            var validationResult = validator.Validate(crew);

            if (validationResult.IsValid)
            {
                await db.CrewRepositiry.UpdateAsync(crew);
                await db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Crew, CrewDto>(crew);
        }

        public async Task DeleteAsync(Guid id)
        {
            await db.CrewRepositiry.DeleteAsync(id);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            await db.CrewRepositiry.DeleteAsync();
            await db.SaveChangesAsync();
        }
    }
}