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


        public CrewDto Get(Guid id)
        {
            return mapper.Map<Crew, CrewDto>(db.CrewRepositiry.Get(id));
        }

        public async Task<List<CrewDto>> GetAllAsync()
        {
            List<Crew> crews = await db.CrewRepositiry.GetAllAsync();
            return mapper.Map<List<Crew>, List<CrewDto>>(crews);
        }

        public CrewDto Create(CrewDto crewDto)
        {
            var crew = mapper.Map<CrewDto, Crew>(crewDto);

            crew.Id = Guid.NewGuid();
            crew.Pilot = db.PilotRepositiry.Get(crewDto.PilotId);
            crew.Stewardesses = db.StewardessRepositiry.FindAsync(i => crewDto.StewardessesId.Contains(i.Id)).Result;

            var validationResult = validator.Validate(crew);

            if (validationResult.IsValid)
            {
                db.CrewRepositiry.Create(crew);
                db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Crew, CrewDto>(crew);
        }

        public CrewDto Update(Guid id, CrewDto crewDto)
        {
            var crew = mapper.Map<CrewDto, Crew>(crewDto);

            crew.Id = id;
            crew.Pilot = db.PilotRepositiry.Get(crewDto.PilotId);
            crew.Stewardesses = db.StewardessRepositiry.FindAsync(i => crewDto.StewardessesId.Contains(i.Id)).Result;

            var validationResult = validator.Validate(crew);

            if (validationResult.IsValid)
            {
                db.CrewRepositiry.Update(crew);
                db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Crew, CrewDto>(crew);
        }

        public void Delete(Guid id)
        {
            db.CrewRepositiry.Delete(id);
            db.SaveChangesAsync();
        }

        public void DeleteAll()
        {
            db.CrewRepositiry.Delete();
            db.SaveChangesAsync();
        }
    }
}