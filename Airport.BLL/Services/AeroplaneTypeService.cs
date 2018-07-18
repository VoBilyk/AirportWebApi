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
    public class AeroplaneTypeService : IAeroplaneTypeService
    {
        private IUnitOfWork db;
        private IMapper mapper;
        IValidator<AeroplaneType> validator;

        public AeroplaneTypeService(IUnitOfWork uow, IMapper mapper, IValidator<AeroplaneType> validator)
        {
            this.db = uow;
            this.mapper = mapper;
            this.validator = validator;
        }


        public AeroplaneTypeDto Get(Guid id)
        {
            return mapper.Map<AeroplaneType, AeroplaneTypeDto>(db.AeroplaneTypeRepository.Get(id));
        }

        public async Task<List<AeroplaneTypeDto>> GetAllAsync()
        {
            var aeroplaneTypes = db.AeroplaneTypeRepository.GetAllAsync();
            return await mapper.Map<Task<List<AeroplaneType>>, Task<List<AeroplaneTypeDto>>>(aeroplaneTypes);
        }

        public AeroplaneTypeDto Create(AeroplaneTypeDto aeroplaneTypeDto)
        {
            var aeroplaneType = mapper.Map<AeroplaneTypeDto, AeroplaneType>(aeroplaneTypeDto);
            aeroplaneType.Id = Guid.NewGuid();

            var validationResult = validator.Validate(aeroplaneType);

            if (validationResult.IsValid)
            {
                db.AeroplaneTypeRepository.Create(aeroplaneType);
                db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<AeroplaneType, AeroplaneTypeDto>(aeroplaneType);
        }

        public AeroplaneTypeDto Update(Guid id, AeroplaneTypeDto aeroplaneTypeDto)
        {
            var aeroplaneType = mapper.Map<AeroplaneTypeDto, AeroplaneType>(aeroplaneTypeDto);
            aeroplaneType.Id = id;

            var validationResult = validator.Validate(aeroplaneType);

            if (validationResult.IsValid)
            {
                db.AeroplaneTypeRepository.Update(aeroplaneType);
                db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<AeroplaneType, AeroplaneTypeDto>(aeroplaneType);
        }

        public void Delete(Guid id)
        {
            db.AeroplaneTypeRepository.Delete(id);
            db.SaveChangesAsync();
        }

        public void DeleteAll()
        {
            db.AeroplaneTypeRepository.Delete();
            db.SaveChangesAsync();
        }
    }
}