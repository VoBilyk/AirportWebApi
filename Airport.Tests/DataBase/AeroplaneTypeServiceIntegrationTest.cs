using NUnit.Framework;
using FluentValidation;
using AutoMapper;
using Airport.BLL.Mappers;
using Airport.DAL.Entities;
using Airport.BLL.Validators;
using Airport.DAL.Interfaces;
using Airport.Shared.DTO;
using Airport.BLL.Services;
using Airport.DAL;
using Airport.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Airport.Tests.IntegrationTests
{
    [TestFixture]
    public class AeroplaneTypeServiceIntegrationTest
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private IValidator<AeroplaneType> validator;
        private IAeroplaneTypeService service;
        private AirportContext db;
        private AeroplaneType testItem;


        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralMapperProfile()));
            mapper = config.CreateMapper();

            var builder = new DbContextOptionsBuilder<AirportContext>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AirportDb(Bilyk);Trusted_Connection=True;");

            db = new AirportContext(builder.Options);
            db.Database.BeginTransaction();

            unitOfWork = new UnitOfWork(db);
            validator = new AeroplaneTypeValidator();
            service = new AeroplaneTypeService(unitOfWork, mapper, validator);

            // Adding test item to db for updating and getting and deleting in tests 
            testItem = Initializer.AeroplaneTypeFaker.Generate();
            db.AeroplaneTypes.Add(testItem);
            db.SaveChanges();
        }


        [Test]
        public void Get_WhenGetElementByKnownId_ThenReturnedNotNullObject()
        {
            // Arrange

            // Act
            var returnedCount = service.GetAsync(testItem.Id).Result;

            // Assert
            Assert.IsNotNull(returnedCount);
        }

        [Test]
        public void GetAll_WhenGetAllElements_ThenReturnedCountIsTheSameLikeInDb()
        {
            // Arrange
            var amountBeforeCreating = db.AeroplaneTypes.Count();

            // Act
            var returnedCount = service.GetAllAsync().Result;

            // Assert
            Assert.AreEqual(returnedCount.Count, amountBeforeCreating);
        }

        [Test]
        public void Create_WhenCreatedNewItem_ThenElementsInDbBecomePlusOne()
        {
            // Arrange
            var item = Initializer.AeroplaneTypeFaker.Generate();
            var dto = mapper.Map<AeroplaneType, AeroplaneTypeDto>(item);
            var amountBeforeCreating = db.AeroplaneTypes.Count();

            // Act
            service.CreateAsync(dto).Wait();

            // Assert
            Assert.AreEqual(amountBeforeCreating + 1, db.AeroplaneTypes.Count());
        }

        [Test]
        public void Delete_WhendeletingItem_ThenElementsInDbBecomeMinusOne()
        {
            // Arrange
            var amountBeforeDeleting = db.AeroplaneTypes.Count();

            // Act
            service.DeleteAsync(testItem.Id).Wait();

            // Assert
            Assert.AreEqual(amountBeforeDeleting - 1, db.AeroplaneTypes.Count());
        }

        [TearDown]
        public void Cleanup()
        {
            db.Database.RollbackTransaction();
        }
    }
}
