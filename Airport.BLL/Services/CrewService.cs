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

        public List<CrewDto> GetAll()
        {
            return mapper.Map<List<Crew>, List<CrewDto>>(db.CrewRepositiry.GetAll());
        }

        public CrewDto Create(CrewDto crewDto)
        {
            var crew = mapper.Map<CrewDto, Crew>(crewDto);

            crew.Id = Guid.NewGuid();
            crew.Pilot = db.PilotRepositiry.Get(crewDto.PilotId);
            crew.Stewardesses = db.StewardessRepositiry.GetAll().Where(i => crewDto.StewardessesId.Contains(i.Id)).ToList();

            var validationResult = validator.Validate(crew);

            if (validationResult.IsValid)
            {
                db.CrewRepositiry.Create(crew);
                db.SaveChanges();
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
            crew.Stewardesses = db.StewardessRepositiry.GetAll().Where(i => crewDto.StewardessesId.Contains(i.Id)).ToList();

            var validationResult = validator.Validate(crew);

            if (validationResult.IsValid)
            {
                db.CrewRepositiry.Update(crew);
                db.SaveChanges();
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
            db.SaveChanges();
        }

        public void DeleteAll()
        {
            db.CrewRepositiry.Delete();
            db.SaveChanges();
        }
    }
}