﻿using System;
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
    public class PilotServiceTest
    {
        private IUnitOfWork unitOfWorkFake;
        private IMapper mapper;
        private IValidator<Pilot> validator;
        private IValidator<Pilot> alwaysValidValidator;


        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralMapperProfile()));
            mapper = config.CreateMapper();

            unitOfWorkFake = A.Fake<IUnitOfWork>();

            validator = new PilotValidator();
            alwaysValidValidator = A.Fake<IValidator<Pilot>>();
            A.CallTo(() => alwaysValidValidator.Validate(A<Pilot>._)).Returns(new ValidationResult());
        }

        [Test]
        public void Create_WhenDtoIsPassed_ThenReturnedTheSameWithCreatedId()
        {
            // Arrange
            var dto = new PilotDto()
            {
                FirstName = "FirstName",
                LastName = "SecondName",
                BirthDate = new DateTime(1980, 1, 1),
                Experience = 5
            };

            var service = new PilotService(unitOfWorkFake, mapper, alwaysValidValidator);

            // Act
            var returnedDto = service.CreateAsync(dto).Result;

            // Assert
            Assert.True(returnedDto.Id != default(Guid).ToString());
            Assert.AreEqual(dto.FirstName, returnedDto.FirstName);
            Assert.AreEqual(dto.LastName, returnedDto.LastName);
            Assert.AreEqual(dto.BirthDate, returnedDto.BirthDate);
            Assert.AreEqual(dto.Experience, returnedDto.Experience);
        }

        [Test]
        public void Create_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var dto = new PilotDto() { };

            var service = new PilotService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<AggregateException>(() => service.CreateAsync(dto).Wait());
        }

        [Test]
        public void Update_WhenDtoIsPassed_ThenReturnedTheSameWithPassedId()
        {
            // Arrange
            var id = Guid.NewGuid();

            var dto = new PilotDto()
            {
                FirstName = "FirstName",
                LastName = "SecondName",
                BirthDate = new DateTime(1980, 1, 1),
                Experience = 5
            };

            var service = new PilotService(unitOfWorkFake, mapper, alwaysValidValidator);

            // Act
            var returnedDto = service.UpdateAsync(id, dto).Result;

            // Assert
            Assert.True(returnedDto.Id == id.ToString());
            Assert.AreEqual(dto.FirstName, returnedDto.FirstName);
            Assert.AreEqual(dto.LastName, returnedDto.LastName);
            Assert.AreEqual(dto.BirthDate, returnedDto.BirthDate);
            Assert.AreEqual(dto.Experience, returnedDto.Experience);
        }

        [Test]
        public void Update_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var id = default(Guid);
            var dto = new PilotDto() { };

            var service = new PilotService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<AggregateException>(() => service.UpdateAsync(id, dto).Wait());
        }
    }
}
