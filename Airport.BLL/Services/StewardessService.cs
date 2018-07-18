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


        public StewardessDto Get(Guid id)
        {
            return mapper.Map<Stewardess, StewardessDto>(db.StewardessRepositiry.Get(id));
        }

        public List<StewardessDto> GetAll()
        {
            return mapper.Map<List<Stewardess>, List<StewardessDto>>(db.StewardessRepositiry.GetAll());
        }

        public StewardessDto Create(StewardessDto stewardessDto)
        {
            var stewardess = mapper.Map<StewardessDto, Stewardess>(stewardessDto);
            stewardess.Id = Guid.NewGuid();

            var validationResult = validator.Validate(stewardess);

            if (validationResult.IsValid)
            {
                db.StewardessRepositiry.Create(stewardess);
                db.SaveChanges();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Stewardess, StewardessDto>(stewardess);
        }

        public StewardessDto Update(Guid id, StewardessDto stewardessDto)
        {
            var stewardess = mapper.Map<StewardessDto, Stewardess>(stewardessDto);
            stewardess.Id = id;

            var validationResult = validator.Validate(stewardess);
            
            if (validationResult.IsValid)
            {
                db.StewardessRepositiry.Update(stewardess);
                db.SaveChanges();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Stewardess, StewardessDto>(stewardess);
        }

        public void Delete(Guid id)
        {
            db.StewardessRepositiry.Delete(id);
            db.SaveChanges();
        }

        public void DeleteAll()
        {
            db.StewardessRepositiry.Delete();
            db.SaveChanges();
        }
    }
}