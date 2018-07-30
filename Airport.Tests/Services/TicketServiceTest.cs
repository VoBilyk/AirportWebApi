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
    public class TicketServiceTest
    {
        private IUnitOfWork unitOfWorkFake;
        private IMapper mapper;
        private IValidator<Ticket> validator;
        private IValidator<Ticket> alwaysValidValidator;


        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralMapperProfile()));
            mapper = config.CreateMapper();

            unitOfWorkFake = A.Fake<IUnitOfWork>();

            validator = new TicketValidator();
            alwaysValidValidator = A.Fake<IValidator<Ticket>>();
            A.CallTo(() => alwaysValidValidator.Validate(A<Ticket>._)).Returns(new ValidationResult());
        }

        [Test]
        public void Create_WhenDtoIsPassed_ThenReturnedTheSameWithCreatedId()
        {
            // Arrange
            var flightId = Guid.NewGuid();

            var dto = new TicketDto()
            {
                FlightId = flightId,
                Price = 100
            };

            A.CallTo(() => unitOfWorkFake.FlightRepository.GetAsync(flightId)).Returns(new Flight { Id = flightId });

            var service = new TicketService(unitOfWorkFake, mapper, alwaysValidValidator);

            // Act
            var returnedDto = service.CreateAsync(dto).Result;

            // Assert
            Assert.True(returnedDto.Id != default(Guid));
            Assert.AreEqual(dto.FlightId, returnedDto.FlightId);
            Assert.AreEqual(dto.Price, returnedDto.Price);
        }

        [Test]
        public void Create_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var dto = new TicketDto() { };

            var service = new TicketService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<AggregateException>(() => service.CreateAsync(dto).Wait());
        }

        [Test]
        public void Update_WhenDtoIsPassed_ThenReturnedTheSameWithPassedId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var flightId = Guid.NewGuid();

            var dto = new TicketDto()
            {
                FlightId = flightId,
                Price = 100
            };

            A.CallTo(() => unitOfWorkFake.FlightRepository.GetAsync(flightId)).Returns(new Flight { Id = flightId });

            var service = new TicketService(unitOfWorkFake, mapper, alwaysValidValidator);

            // Act
            var returnedDto = service.UpdateAsync(id, dto).Result;

            // Assert
            Assert.True(returnedDto.Id != default(Guid));
            Assert.AreEqual(dto.FlightId, returnedDto.FlightId);
            Assert.AreEqual(dto.Price, returnedDto.Price);
        }

        [Test]
        public void Update_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var id = default(Guid);
            var dto = new TicketDto() { };

            var service = new TicketService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<AggregateException>(() => service.UpdateAsync(id, dto).Wait());
        }
    }
}
