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


        public async Task<AeroplaneDto> GetAsync(Guid id)
        {
            Aeroplane aeroplane = await db.AeroplaneRepository.GetAsync(id);
            return mapper.Map<Aeroplane, AeroplaneDto>(aeroplane);
        }

        public async Task<List<AeroplaneDto>> GetAllAsync()
        {
            var aeroplanes = db.AeroplaneRepository.GetAllAsync();
            return await mapper.Map<Task<List<Aeroplane>>, Task<List<AeroplaneDto>>>(aeroplanes);
        }

        public async Task<AeroplaneDto> CreateAsync(AeroplaneDto aeroplaneDto)
        {
            var aeroplane = mapper.Map<AeroplaneDto, Aeroplane>(aeroplaneDto);

            aeroplane.Id = Guid.NewGuid();
            aeroplane.AeroplaneType = await db.AeroplaneTypeRepository.GetAsync(aeroplaneDto.AeroplaneTypeId);

            var validationResult = validator.Validate(aeroplane);

            if (validationResult.IsValid)
            {
                await db.AeroplaneRepository.CreateAsync(aeroplane);
                db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Aeroplane, AeroplaneDto>(aeroplane);
        }

        public async Task<AeroplaneDto> UpdateAsync(Guid id, AeroplaneDto aeroplaneDto)
        {
            var aeroplane = mapper.Map<AeroplaneDto, Aeroplane>(aeroplaneDto);

            aeroplane.Id = id;
            aeroplane.AeroplaneType = await db.AeroplaneTypeRepository.GetAsync(aeroplaneDto.AeroplaneTypeId);

            var validationResult = validator.Validate(aeroplane);
            
            if (validationResult.IsValid)
            {
                await db.AeroplaneRepository.UpdateAsync(aeroplane);
                db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Aeroplane, AeroplaneDto>(aeroplane);
        }

        public async Task DeleteAsync(Guid id)
        {
            await db.AeroplaneRepository.DeleteAsync(id);
            db.SaveChangesAsync();
        }

        public async Task DeleteAll()
        {
            await db.AeroplaneRepository.DeleteAsync();
            db.SaveChangesAsync();
        }
    }
}
