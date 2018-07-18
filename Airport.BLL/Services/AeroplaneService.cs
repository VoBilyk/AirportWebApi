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
    public class AeroplaneService : IAeroplaneService
    {
        private IUnitOfWork db;
        private IMapper mapper;
        private IValidator<Aeroplane> validator;

        public AeroplaneService(IUnitOfWork uow, IMapper mapper, IValidator<Aeroplane> validator)
        {
            this.db = uow;
            this.mapper = mapper;
            this.validator = validator;
        }


        public AeroplaneDto Get(Guid id)
        {
            return mapper.Map<Aeroplane, AeroplaneDto>(db.AeroplaneRepository.Get(id));
        }

        public List<AeroplaneDto> GetAll()
        {
            return mapper.Map<List<Aeroplane>, List<AeroplaneDto>>(db.AeroplaneRepository.GetAll());
        }

        public AeroplaneDto Create(AeroplaneDto aeroplaneDto)
        {
            var aeroplane = mapper.Map<AeroplaneDto, Aeroplane>(aeroplaneDto);

            aeroplane.Id = Guid.NewGuid();
            aeroplane.AeroplaneType = db.AeroplaneTypeRepository.Get(aeroplaneDto.AeroplaneTypeId);

            var validationResult = validator.Validate(aeroplane);

            if (validationResult.IsValid)
            {
                db.AeroplaneRepository.Create(aeroplane);
                db.SaveChanges();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Aeroplane, AeroplaneDto>(aeroplane);
        }

        public AeroplaneDto Update(Guid id, AeroplaneDto aeroplaneDto)
        {
            var aeroplane = mapper.Map<AeroplaneDto, Aeroplane>(aeroplaneDto);

            aeroplane.Id = id;
            aeroplane.AeroplaneType = db.AeroplaneTypeRepository.Get(aeroplaneDto.AeroplaneTypeId);

            var validationResult = validator.Validate(aeroplane);
            
            if (validationResult.IsValid)
            {
                db.AeroplaneRepository.Update(aeroplane);
                db.SaveChanges();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Aeroplane, AeroplaneDto>(aeroplane);
        }

        public void Delete(Guid id)
        {
            db.AeroplaneRepository.Delete(id);
            db.SaveChanges();
        }

        public void DeleteAll()
        {
            db.AeroplaneRepository.Delete();
            db.SaveChanges();
        }
    }
}
