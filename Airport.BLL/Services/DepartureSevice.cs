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
    public class DepartureService : IDepartureService
    {
        private IUnitOfWork db;
        private IMapper mapper;
        IValidator<Departure> validator;

        public DepartureService(IUnitOfWork uow, IMapper mapper, IValidator<Departure> validator)
        {
            this.db = uow;
            this.mapper = mapper;
            this.validator = validator;
        }


        public DepartureDto Get(Guid id)
        {
            return mapper.Map<Departure, DepartureDto>(db.DepartureRepository.Get(id));
        }

        public async Task<List<DepartureDto>> GetAllAsync()
        {
            var departures = db.DepartureRepository.GetAllAsync();
            return await mapper.Map<Task<List<Departure>>, Task<List<DepartureDto>>>(departures);
        }

        public DepartureDto Create(DepartureDto departureDto)
        {
            var departure = mapper.Map<DepartureDto, Departure>(departureDto);
            departure.Id = Guid.NewGuid();
            departure.Crew = db.CrewRepositiry.Get(departureDto.CrewId);
            departure.Airplane = db.AeroplaneRepository.Get(departureDto.AirplaneId);

            var validationResult = validator.Validate(departure);

            if (validationResult.IsValid)
            {
                db.DepartureRepository.Create(departure);
                db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Departure, DepartureDto>(departure);
        }

        public DepartureDto Update(Guid id, DepartureDto departureDto)
        {
            var departure = mapper.Map<DepartureDto, Departure>(departureDto);
            departure.Id = id;
            departure.Airplane = db.AeroplaneRepository.Get(departureDto.AirplaneId);
            departure.Crew = db.CrewRepositiry.Get(departureDto.CrewId);

            var validationResult = validator.Validate(departure);

            if (validationResult.IsValid)
            {
                db.DepartureRepository.Update(departure);
                db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Departure, DepartureDto>(departure);
        }

        public void Delete(Guid id)
        {
            db.DepartureRepository.Delete(id);
            db.SaveChangesAsync();
        }

        public void DeleteAll()
        {
            db.DepartureRepository.Delete();
            db.SaveChangesAsync();
        }
    }
}