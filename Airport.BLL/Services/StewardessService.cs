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
    public class StewardessService : IStewardessService
    {
        private IUnitOfWork db;
        private IMapper mapper;
        private IValidator<Stewardess> validator;
        
        public StewardessService(IUnitOfWork uow, IMapper mapper, IValidator<Stewardess> validator)
        {
            this.db = uow;
            this.mapper = mapper;
            this.validator = validator;
        }


        public async Task<StewardessDto> GetAsync(Guid id)
        {
            Stewardess stewardess = await db.StewardessRepositiry.GetAsync(id);
            return mapper.Map<Stewardess, StewardessDto>(stewardess);
        }

        public async Task<List<StewardessDto>> GetAllAsync()
        {
            var stewardesses = db.StewardessRepositiry.GetAllAsync();
            return await mapper.Map<Task<List<Stewardess>>, Task<List<StewardessDto>>>(stewardesses);
        }

        public async Task<StewardessDto> CreateAsync(StewardessDto stewardessDto)
        {
            var stewardess = mapper.Map<StewardessDto, Stewardess>(stewardessDto);
            stewardess.Id = Guid.NewGuid();

            var validationResult = validator.Validate(stewardess);

            if (validationResult.IsValid)
            {
                await db.StewardessRepositiry.CreateAsync(stewardess);
                db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Stewardess, StewardessDto>(stewardess);
        }

        public async Task<StewardessDto> UpdateAsync(Guid id, StewardessDto stewardessDto)
        {
            var stewardess = mapper.Map<StewardessDto, Stewardess>(stewardessDto);
            stewardess.Id = id;

            var validationResult = validator.Validate(stewardess);
            
            if (validationResult.IsValid)
            {
                await db.StewardessRepositiry.UpdateAsync(stewardess);
                db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Stewardess, StewardessDto>(stewardess);
        }

        public async Task DeleteAsync(Guid id)
        {
            await db.StewardessRepositiry.DeleteAsync(id);
            db.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            await db.StewardessRepositiry.DeleteAsync();
            db.SaveChangesAsync();
        }
    }
}