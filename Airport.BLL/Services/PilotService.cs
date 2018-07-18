using System;
using System.Collections.Generic;
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


        public PilotDto Get(Guid id)
        {
            return mapper.Map<Pilot, PilotDto>(db.PilotRepositiry.Get(id));
        }

        public List<PilotDto> GetAll()
        {
            return mapper.Map<List<Pilot>, List<PilotDto>>(db.PilotRepositiry.GetAll());
        }

        public PilotDto Create(PilotDto pilotDto)
        {
            var pilot = mapper.Map<PilotDto, Pilot>(pilotDto);
            pilot.Id = Guid.NewGuid();

            var validationResult = validator.Validate(pilot);

            if (validationResult.IsValid)
            {
                db.PilotRepositiry.Create(pilot);
                db.SaveChanges();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Pilot, PilotDto>(pilot);
        }

        public PilotDto Update(Guid id, PilotDto pilotDto)
        {
            var pilot = mapper.Map<PilotDto, Pilot>(pilotDto);
            pilot.Id = id;

            var validationResult = validator.Validate(pilot);

            if (validationResult.IsValid)
            {
                db.PilotRepositiry.Update(pilot);
                db.SaveChanges();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Pilot, PilotDto>(pilot);
        }

        public void Delete(Guid id)
        {
            db.PilotRepositiry.Delete(id);
            db.SaveChanges();
        }

        public void DeleteAll()
        {
            db.PilotRepositiry.Delete();
            db.SaveChanges();
        }
    }
}