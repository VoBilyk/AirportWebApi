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
using System.Collections.Generic;


namespace Airport.Tests.Services
{
    [TestFixture]
    public class CrewServiceTest
    {
        private IUnitOfWork unitOfWorkFake;
        private IMapper mapper;
        private IValidator<Crew> validator;
        private IValidator<Crew> alwaysValidValidator;


        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralMapperProfile()));
            mapper = config.CreateMapper();

            unitOfWorkFake = A.Fake<IUnitOfWork>();

            validator = new CrewValidator();
            alwaysValidValidator = A.Fake<IValidator<Crew>>();
            A.CallTo(() => alwaysValidValidator.Validate(A<Crew>._)).Returns(new ValidationResult());
        }

        [Test]
        public void Create_WhenDtoIsPassed_ThenReturnedTheSameWithCreatedId()
        {
            // Arrange
            var pilotId = Guid.NewGuid();
            var stewardessesId = new List<Guid> { Guid.NewGuid() };

            var dto = new CrewDto()
            {
                PilotId = pilotId,
                StewardessesId = stewardessesId
            };

            A.CallTo(() => unitOfWorkFake.PilotRepositiry.GetAsync(pilotId)).Returns(new Pilot { Id = pilotId });

            A.CallTo(() => unitOfWorkFake.StewardessRepositiry.GetAllAsync())
                .Returns(new List<Stewardess> { new Stewardess { Id = stewardessesId[0] } });

            var service = new CrewService(unitOfWorkFake, mapper, alwaysValidValidator);

            // Act
            var returnedDto = service.CreateAsync(dto).Result;

            // Assert
            Assert.True(returnedDto.Id != default(Guid));
            Assert.AreEqual(dto.PilotId, returnedDto.PilotId);
            Assert.AreEqual(dto.StewardessesId.Count, returnedDto.StewardessesId.Count);

            foreach (var item in dto.StewardessesId)
            {
                Assert.Contains(item, returnedDto.StewardessesId);
            }
        }

        [Test]
        public void Create_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var dto = new CrewDto() { };

            var service = new CrewService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<ValidationException>(() => service.CreateAsync(dto));
        }

        [Test]
        public void Update_WhenDtoIsPassed_ThenReturnedTheSameWithPassedId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var pilotId = Guid.NewGuid();
            var stewardessesId = new List<Guid> { Guid.NewGuid() };

            var dto = new CrewDto()
            {
                PilotId = pilotId,
                StewardessesId = stewardessesId
            };

            A.CallTo(() => unitOfWorkFake.PilotRepositiry.GetAsync(pilotId)).Returns(new Pilot { Id = pilotId });

            A.CallTo(() => unitOfWorkFake.StewardessRepositiry.GetAllAsync())
                .Returns(new List<Stewardess> { new Stewardess { Id = stewardessesId[0] } });

            var service = new CrewService(unitOfWorkFake, mapper, alwaysValidValidator);

            // Act
            var returnedDto = service.UpdateAsync(id, dto).Result;

            // Assert
            Assert.True(returnedDto.Id != default(Guid));
            Assert.AreEqual(dto.PilotId, returnedDto.PilotId);
            Assert.AreEqual(dto.StewardessesId.Count, returnedDto.StewardessesId.Count);

            foreach (var item in dto.StewardessesId)
            {
                Assert.Contains(item, returnedDto.StewardessesId);
            }
        }

        [Test]
        public void Update_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var id = default(Guid);
            var dto = new CrewDto() { };

            var service = new CrewService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<ValidationException>(() => service.UpdateAsync(id, dto));
        }
    }
}
