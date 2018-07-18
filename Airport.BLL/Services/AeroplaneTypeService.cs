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


        public async Task<AeroplaneTypeDto> GetAsync(Guid id)
        {
            AeroplaneType item = await db.AeroplaneTypeRepository.GetAsync(id);
            return mapper.Map<AeroplaneType, AeroplaneTypeDto>(item);
        }

        public async Task<List<AeroplaneTypeDto>> GetAllAsync()
        {
            var aeroplaneTypes = db.AeroplaneTypeRepository.GetAllAsync();
            return await mapper.Map<Task<List<AeroplaneType>>, Task<List<AeroplaneTypeDto>>>(aeroplaneTypes);
        }

        public async Task<AeroplaneTypeDto> CreateAsync(AeroplaneTypeDto aeroplaneTypeDto)
        {
            var aeroplaneType = mapper.Map<AeroplaneTypeDto, AeroplaneType>(aeroplaneTypeDto);
            aeroplaneType.Id = Guid.NewGuid();

            var validationResult = validator.Validate(aeroplaneType);

            if (validationResult.IsValid)
            {
                await db.AeroplaneTypeRepository.CreateAsync(aeroplaneType);
                await db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<AeroplaneType, AeroplaneTypeDto>(aeroplaneType);
        }

        public async Task<AeroplaneTypeDto> UpdateAsync(Guid id, AeroplaneTypeDto aeroplaneTypeDto)
        {
            var aeroplaneType = mapper.Map<AeroplaneTypeDto, AeroplaneType>(aeroplaneTypeDto);
            aeroplaneType.Id = id;

            var validationResult = validator.Validate(aeroplaneType);

            if (validationResult.IsValid)
            {
                await db.AeroplaneTypeRepository.UpdateAsync(aeroplaneType);
                await db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<AeroplaneType, AeroplaneTypeDto>(aeroplaneType);
        }

        public async Task DeleteAsync(Guid id)
        {
            await db.AeroplaneTypeRepository.DeleteAsync(id);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            await db.AeroplaneTypeRepository.DeleteAsync();
            await db.SaveChangesAsync();
        }
    }
}