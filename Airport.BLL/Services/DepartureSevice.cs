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


        public async Task<DepartureDto> GetAsync(Guid id)
        {
            Departure item = await db.DepartureRepository.GetAsync(id);
            return mapper.Map<Departure, DepartureDto>(item);
        }

        public async Task<List<DepartureDto>> GetAllAsync()
        {
            var departures = await db.DepartureRepository.GetAllAsync();
            return mapper.Map<List<Departure>, List<DepartureDto>>(departures);
        }

        public async Task<DepartureDto> CreateAsync(DepartureDto departureDto)
        {
            var departure = mapper.Map<DepartureDto, Departure>(departureDto);
            departure.Id = Guid.NewGuid();
            departure.Crew = await db.CrewRepositiry.GetAsync(departureDto.CrewId);
            departure.Airplane = await db.AeroplaneRepository.GetAsync(departureDto.AirplaneId);

            var validationResult = validator.Validate(departure);

            if (validationResult.IsValid)
            {
                await db.DepartureRepository.CreateAsync(departure);
                await db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Departure, DepartureDto>(departure);
        }

        public async Task<DepartureDto> UpdateAsync(Guid id, DepartureDto departureDto)
        {
            var departure = mapper.Map<DepartureDto, Departure>(departureDto);
            departure.Id = id;
            departure.Airplane = await db.AeroplaneRepository.GetAsync(departureDto.AirplaneId);
            departure.Crew = await db.CrewRepositiry.GetAsync(departureDto.CrewId);

            var validationResult = validator.Validate(departure);

            if (validationResult.IsValid)
            {
                await db.DepartureRepository.UpdateAsync(departure);
                await db.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }

            return mapper.Map<Departure, DepartureDto>(departure);
        }

        public async Task DeleteAsync(Guid id)
        {
            await db.DepartureRepository.DeleteAsync(id);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            await db.DepartureRepository.DeleteAsync();
            await db.SaveChangesAsync();
        }
    }
}