using System;
using NUnit.Framework;
using FakeItEasy;
using FluentValidation;
using FluentValidation.Results;
using AutoMapper;
using Airport.BLL.Mappers;
using Airport.DAL.Entities;
using Airport.BLL.Validators;
using Airport.DAL.Interfaces;
using Airport.Shared.DTO;
using Airport.BLL.Services;

namespace Airport.Tests.Services
{
    [TestFixture]
    public class DepartureServiceTest
    {
        private IUnitOfWork unitOfWorkFake;
        private IMapper mapper;
        private IValidator<Departure> validator;
        private IValidator<Departure> alwaysValidValidator;


        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralMapperProfile()));
            mapper = config.CreateMapper();

            unitOfWorkFake = A.Fake<IUnitOfWork>();

            validator = new DepartureValidator();
            alwaysValidValidator = A.Fake<IValidator<Departure>>();
            A.CallTo(() => alwaysValidValidator.Validate(A<Departure>._)).Returns(new ValidationResult());
        }

        [Test]
        public void Create_WhenDtoIsPassed_ThenReturnedTheSameWithCreatedId()
        {
            // Arrange
            var crewId = Guid.NewGuid();
            var airplaneId = Guid.NewGuid();
            
            var dto = new DepartureDto()
            {
                Time = new DateTime(2018, 7, 17, 13, 0, 0),
                CrewId = crewId,
                AirplaneId = airplaneId
            };

            A.CallTo(() => unitOfWorkFake.CrewRepositiry.Get(crewId)).Returns(new Crew { Id = crewId });
            A.CallTo(() => unitOfWorkFake.AeroplaneRepository.Get(airplaneId)).Returns(new Aeroplane { Id = airplaneId });


            var service = new DepartureService(unitOfWorkFake, mapper, alwaysValidValidator);

            // Act
            var returnedDto = service.Create(dto);

            // Assert
            Assert.True(returnedDto.Id != default(Guid));
            Assert.AreEqual(dto.CrewId, returnedDto.CrewId);
            Assert.AreEqual(dto.AirplaneId, returnedDto.AirplaneId);
            Assert.AreEqual(dto.Time, returnedDto.Time);
        }

        [Test]
        public void Create_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var dto = new DepartureDto() { };

            var service = new DepartureService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<ValidationException>(() => service.Create(dto));
        }

        [Test]
        public void Update_WhenDtoIsPassed_ThenReturnedTheSameWithPassedId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var crewId = Guid.NewGuid();
            var airplaneId = Guid.NewGuid();

            var dto = new DepartureDto()
            {
                Time = new DateTime(2018, 7, 17, 13, 0, 0),
                CrewId = crewId,
                AirplaneId = airplaneId
            };

            A.CallTo(() => unitOfWorkFake.CrewRepositiry.Get(crewId)).Returns(new Crew { Id = crewId });
            A.CallTo(() => unitOfWorkFake.AeroplaneRepository.Get(airplaneId)).Returns(new Aeroplane { Id = airplaneId });

            var service = new DepartureService(unitOfWorkFake, mapper, alwaysValidValidator);

            // Act
            var returnedDto = service.Update(id, dto);

            // Assert
            Assert.True(returnedDto.Id != default(Guid));
            Assert.AreEqual(dto.CrewId, returnedDto.CrewId);
            Assert.AreEqual(dto.AirplaneId, returnedDto.AirplaneId);
            Assert.AreEqual(dto.Time, returnedDto.Time);
        }

        [Test]
        public void Update_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var id = default(Guid);
            var dto = new DepartureDto() { };

            var service = new DepartureService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<ValidationException>(() => service.Update(id, dto));
        }
    }
}
