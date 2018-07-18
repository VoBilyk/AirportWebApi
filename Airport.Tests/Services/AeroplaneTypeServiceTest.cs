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
    public class AeroplaneTypeServiceTest
    {
        private IUnitOfWork unitOfWorkFake;
        private IMapper mapper;
        private IValidator<AeroplaneType> validator;
        private IValidator<AeroplaneType> alwaysValidValidator;


        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralMapperProfile()));
            mapper = config.CreateMapper();

            unitOfWorkFake = A.Fake<IUnitOfWork>();

            validator = new AeroplaneTypeValidator();
            alwaysValidValidator = A.Fake<IValidator<AeroplaneType>>();
            A.CallTo(() => alwaysValidValidator.Validate(A<AeroplaneType>._)).Returns(new ValidationResult());
        }

        [Test]
        public void Create_WhenDtoIsPassed_ThenReturnedTheSameWithCreatedId()
        {
            // Arrange
            var dto = new AeroplaneTypeDto()
            {
                Model = "Boeing-747",
                Carrying = 100000,
                Places = 200
            };

            var service = new AeroplaneTypeService(unitOfWorkFake, mapper, alwaysValidValidator);

            // Act
            var returnedDto = service.CreateAsync(dto).Result;

            // Assert
            Assert.True(returnedDto.Id != default(Guid));
            Assert.AreEqual(dto.Model, returnedDto.Model);
            Assert.AreEqual(dto.Carrying, returnedDto.Carrying);
            Assert.AreEqual(dto.Places, returnedDto.Places);
        }

        [Test]
        public void Create_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var dto = new AeroplaneTypeDto() { };

            var service = new AeroplaneTypeService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<ValidationException>(() => service.CreateAsync(dto));
        }

        [Test]
        public void Update_WhenDtoIsPassed_ThenReturnedTheSameWithPassedId()
        {
            // Arrange
            var id = Guid.NewGuid();

            var dto = new AeroplaneTypeDto()
            {
                Model = "Boeing-747",
                Carrying = 100000,
                Places = 200
            };

            var service = new AeroplaneTypeService(unitOfWorkFake, mapper, alwaysValidValidator);

            // Act
            var returnedDto = service.UpdateAsync(id, dto).Result;

            // Assert
            Assert.True(returnedDto.Id != default(Guid));
            Assert.AreEqual(dto.Model, returnedDto.Model);
            Assert.AreEqual(dto.Carrying, returnedDto.Carrying);
            Assert.AreEqual(dto.Places, returnedDto.Places);
        }

        [Test]
        public void Update_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var id = default(Guid);
            var dto = new AeroplaneTypeDto() { };

            var service = new AeroplaneTypeService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<ValidationException>(() => service.UpdateAsync(id, dto));
        }
    }
}
