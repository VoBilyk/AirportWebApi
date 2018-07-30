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
    public class FlightServiceTest
    {
        private IUnitOfWork unitOfWorkFake;
        private IMapper mapper;
        private IValidator<Flight> validator;
        private IValidator<Flight> alwaysValidValidator;


        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralMapperProfile()));
            mapper = config.CreateMapper();

            unitOfWorkFake = A.Fake<IUnitOfWork>();

            validator = new FlightValidator();
            alwaysValidValidator = A.Fake<IValidator<Flight>>();
            A.CallTo(() => alwaysValidValidator.Validate(A<Flight>._)).Returns(new ValidationResult());
        }

        [Test]
        public void Create_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var dto = new FlightDto() { };

            var service = new FlightService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<AggregateException>(() => service.CreateAsync(dto).Wait());
        }

        [Test]
        public void Update_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var id = default(Guid);
            var dto = new FlightDto() { };

            var service = new FlightService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<AggregateException>(() => service.UpdateAsync(id, dto).Wait());
        }
    }
}
