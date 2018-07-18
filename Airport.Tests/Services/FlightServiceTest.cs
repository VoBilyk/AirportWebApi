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
        public void Create_WhenDtoIsPassed_ThenReturnedTheSameWithCreatedId()
        {
            // Arrange
            var ticketsId = new List<Guid>
                {
                    Guid.NewGuid()
                };

            var dto = new FlightDto()
            {
                Name = "AAA-111",
                Destinition = "B",
                DeparturePoint = "A",
                ArrivalTime = new DateTime(2018, 07, 17, 13, 0, 0),
                DepartureTime = new DateTime(2018, 07, 17, 14, 0, 0),
                TicketsId = ticketsId
            };

            A.CallTo(() => unitOfWorkFake.TicketRepository.GetAll())
                .Returns(new List<Ticket> { new Ticket { Id = ticketsId[0] } });

            var service = new FlightService(unitOfWorkFake, mapper, alwaysValidValidator);

            // Act
            var returnedDto = service.Create(dto);

            // Assert
            Assert.True(returnedDto.Id != default(Guid));
            Assert.AreEqual(dto.Name, returnedDto.Name);
            Assert.AreEqual(dto.Destinition, returnedDto.Destinition);
            Assert.AreEqual(dto.DepartureTime, returnedDto.DepartureTime);
            Assert.AreEqual(dto.DeparturePoint, returnedDto.DeparturePoint);
            Assert.AreEqual(dto.TicketsId.Count, returnedDto.TicketsId.Count);

            foreach (var item in dto.TicketsId)
            {
                Assert.Contains(item, returnedDto.TicketsId);
            }
        }

        [Test]
        public void Create_WhenDtoIsEmpty_ThenThrowValidExeption()
        {
            // Arrange
            var dto = new FlightDto() { };

            var service = new FlightService(unitOfWorkFake, mapper, validator);

            // Act

            // Assert
            Assert.Throws<ValidationException>(() => service.Create(dto));
        }

        [Test]
        public void Update_WhenDtoIsPassed_ThenReturnedTheSameWithPassedId()
        {
            // Arrange
            var id = Guid.NewGuid();

            var ticketsId = new List<Guid>
                {
                    Guid.NewGuid()
                };

            var dto = new FlightDto()
            {
                Name = "AAA-111",
                Destinition = "B",
                DeparturePoint = "A",
                ArrivalTime = new DateTime(2018, 07, 17, 13, 0, 0),
                DepartureTime = new DateTime(2018, 07, 17, 14, 0, 0),
                TicketsId = ticketsId
            };

            A.CallTo(() => unitOfWorkFake.TicketRepository.GetAll())
                .Returns(new List<Ticket> { new Ticket { Id = ticketsId[0] } });

            var service = new FlightService(unitOfWorkFake, mapper, alwaysValidValidator);

            // Act
            var returnedDto = service.Update(id, dto);

            // Assert
            Assert.True(returnedDto.Id != default(Guid));
            Assert.AreEqual(dto.Name, returnedDto.Name);
            Assert.AreEqual(dto.Destinition, returnedDto.Destinition);
            Assert.AreEqual(dto.DepartureTime, returnedDto.DepartureTime);
            Assert.AreEqual(dto.DeparturePoint, returnedDto.DeparturePoint);
            Assert.AreEqual(dto.TicketsId.Count, returnedDto.TicketsId.Count);

            foreach (var item in dto.TicketsId)
            {
                Assert.Contains(item, returnedDto.TicketsId);
            }
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
            Assert.Throws<ValidationException>(() => service.Update(id, dto));
        }
    }
}
