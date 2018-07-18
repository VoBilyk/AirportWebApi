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
    public class StewardessServiceTest
    {
        private IUnitOfWork unitOfWorkFake;
        private IMapper mapper;
        private IValidator<Stewardess> validator;
        private IValidator<Stewardess> alwaysValidValidator;


        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralMapperProfile()));
            mapper = config.CreateMapper();
                        
            unitOfWorkFake = A.Fake<IUnitOfWork>();

            validator = new StewardessValidator();
            alwaysValidValidator = A.Fake<IValidator<Stewardess>>();
            A.CallTo(() => alwaysValidValidator.Validate(A<Stewardess>._)).Returns(new ValidationResult());
        }

        [Test]
        public void Create_WhenDtoIsPassed_ThenReturnedTheSameWithCreatedId()
        {
            // Arrange
            var dto = new StewardessDto()
            {
                FirstName = "FirstName",
                SecondName = "SecondName",
                BirthDate = new DateTime(1990, 1, 1)
            };

            var service = new StewardessService(unitOfWorkFake, mapper, alwaysValidValidator);

            // Act
            var returnedDto = service.Create(dto);

            // Assert
            Assert.True(returnedDto.Id != default(Guid));
            Assert.AreEqual(dto.FirstName, returnedDto.FirstName);
            Assert.AreEqual(dto.SecondName, returnedDto.SecondName);
            Assert.AreEqual(dto.BirthDate, returnedDto.BirthDate);
        }

        [Test]
        public void Create_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var dto = new StewardessDto() { };

            var service = new StewardessService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<ValidationException>(() => service.Create(dto));
        }

        [Test]
        public void Update_WhenDtoIsPassed_ThenReturnedTheSameWithPassedId()
        {
            // Arrange
            var id = Guid.NewGuid();

            var dto = new StewardessDto()
            {
                FirstName = "FirstName",
                SecondName = "SecondName",
                BirthDate = new DateTime(1990, 1, 1)
            };

            var service = new StewardessService(unitOfWorkFake, mapper, alwaysValidValidator);

            // Act
            var returnedDto = service.Update(id, dto);

            // Assert
            Assert.True(returnedDto.Id == id);
            Assert.AreEqual(dto.FirstName, returnedDto.FirstName);
            Assert.AreEqual(dto.SecondName, returnedDto.SecondName);
            Assert.AreEqual(dto.BirthDate, returnedDto.BirthDate);
        }

        [Test]
        public void Update_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var id = default(Guid);
            var dto = new StewardessDto() { };

            var service = new StewardessService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<ValidationException>(() => service.Update(id, dto));
        }
    }
}
