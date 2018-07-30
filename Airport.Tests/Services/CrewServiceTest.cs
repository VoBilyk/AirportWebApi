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
        public void Create_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var dto = new CrewDto() { };

            var service = new CrewService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<AggregateException>(() => service.CreateAsync(dto).Wait());
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
            Assert.Throws<AggregateException>(() => service.UpdateAsync(id, dto).Wait());
        }
    }
}
