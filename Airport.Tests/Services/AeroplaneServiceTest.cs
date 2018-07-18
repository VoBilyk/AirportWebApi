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
    public class AeroplaneServiceTest
    {
        private IUnitOfWork unitOfWorkFake;
        private IMapper mapper;
        private IValidator<Aeroplane> validator;
        private IValidator<Aeroplane> alwaysValidValidator;


        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralMapperProfile()));
            mapper = config.CreateMapper();

            unitOfWorkFake = A.Fake<IUnitOfWork>();

            validator = new AeroplaneValidator();
            alwaysValidValidator = A.Fake<IValidator<Aeroplane>>();
            A.CallTo(() => alwaysValidValidator.Validate(A<Aeroplane>._)).Returns(new ValidationResult());
        }

        [Test]
        public void Create_WhenDtoIsPassed_ThenReturnedTheSameWithCreatedId()
        {
            // Arrange
            var aeroplaneTypeId = Guid.NewGuid();

            var dto = new AeroplaneDto()
            {
                AeroplaneTypeId = aeroplaneTypeId,
                Name = "Boeing-747",
                Lifetime = new TimeSpan(10,0,0)
            };

            A.CallTo(() => unitOfWorkFake.AeroplaneTypeRepository.Get(aeroplaneTypeId))
                .Returns(new AeroplaneType { Id = aeroplaneTypeId });

            var service = new AeroplaneService(unitOfWorkFake, mapper, alwaysValidValidator);
            
            // Act
            var returnedDto = service.Create(dto);

            // Assert
            Assert.True(returnedDto.Id != default(Guid));
            Assert.AreEqual(dto.Name, returnedDto.Name);
            Assert.AreEqual(dto.Lifetime, returnedDto.Lifetime);
            Assert.AreEqual(dto.AeroplaneTypeId, returnedDto.AeroplaneTypeId);
        }

        [Test]
        public void Create_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var dto = new AeroplaneDto() { };

            var service = new AeroplaneService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<ValidationException>(() => service.Create(dto));
        }

        [Test]
        public void Update_WhenDtoIsPassed_ThenReturnedTheSameWithPassedId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var aeroplaneTypeId = Guid.NewGuid();

            var dto = new AeroplaneDto()
            {
                AeroplaneTypeId = aeroplaneTypeId,
                Name = "Boeing-747",
                Lifetime = new TimeSpan(10, 0, 0)
            };

            A.CallTo(() => unitOfWorkFake.AeroplaneTypeRepository.Get(aeroplaneTypeId))
                .Returns(new AeroplaneType { Id = aeroplaneTypeId });

            var service = new AeroplaneService(unitOfWorkFake, mapper, alwaysValidValidator);

            // Act
            var returnedDto = service.Update(id, dto);

            // Assert
            Assert.True(returnedDto.Id == id);
            Assert.True(returnedDto.Id != default(Guid));
            Assert.AreEqual(dto.Name, returnedDto.Name);
            Assert.AreEqual(dto.Lifetime, returnedDto.Lifetime);
            Assert.AreEqual(dto.AeroplaneTypeId, returnedDto.AeroplaneTypeId);
        }

        [Test]
        public void Update_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var id = default(Guid);
            var dto = new AeroplaneDto() { };

            var service = new AeroplaneService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<ValidationException>(() => service.Update(id, dto));
        }
    }
}
