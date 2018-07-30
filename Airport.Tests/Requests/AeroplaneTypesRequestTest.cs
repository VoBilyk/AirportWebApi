using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Airport.Shared.DTO;
using Airport.DAL;
using Airport.DAL.Entities;


namespace Airport.Tests.Requests
{
    [TestFixture]
    public class AeroplaneTypesRequestTest
    {
        private string serverName;
        private AirportContext db;
        private AeroplaneTypeDto testDto;
        private AeroplaneType addedItem;


        [SetUp]
        public void Setup()
        {
            serverName = "http://localhost:57338";

            var builder = new DbContextOptionsBuilder<AirportContext>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AirportDb(Test);Trusted_Connection=True;");

            addedItem = Initializer.AeroplaneTypeFaker.Generate();

            testDto = new AeroplaneTypeDto
            {
                Id = addedItem.Id,
                Carrying = addedItem.Carrying,
                Model = addedItem.Model,
                Places = addedItem.Places
            };

            db = new AirportContext(builder.Options);

            // Adding test item to db
            db.AeroplaneTypes.Add(addedItem);
            db.SaveChanges();
        }

        [Test]
        public void GetAll_WhenRequestGetAll_ThenStatusCode200()
        {
            // Arrange
            HttpResponseMessage response;

            // Act
            using (var client = new HttpClient())
            {
                response = client.GetAsync(serverName + "/api/AeroplaneTypes").Result;
            }

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public void Get_WhenGetItemById_ThenReturnedItemAndStatusCode200()
        {
            // Arrange
            HttpResponseMessage response;

            // Act
            using (var client = new HttpClient())
            {
                response = client.GetAsync($"{serverName}/api/AeroplaneTypes/{testDto.Id}").Result;
            }

            string contextResponse = response.Content.ReadAsStringAsync().Result;
            var resultItem = JsonConvert.DeserializeObject<AeroplaneTypeDto>(contextResponse);

            // Assert
            Assert.IsNotNull(resultItem);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(testDto.Id, resultItem.Id);
        }

        [Test]
        public void Update_WhenUpdateItemWithUknownId_ThenReturnedBadRequest()
        {
            // Arrange
            var updateDto = new AeroplaneTypeDto
            {
                Carrying = 100000,
                Model = "NewModelName",
                Places = 50
            };
            
            // Act
            HttpResponseMessage updatingResponse;
            var jsonUpdatingRequest = JsonConvert.SerializeObject(updateDto);
            var putContext = new StringContent(jsonUpdatingRequest, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                updatingResponse = client.PutAsync($"{serverName}/api/AeroplaneTypes/{Guid.NewGuid()}", putContext).Result;
            }

            // Assert
            Assert.AreEqual(updatingResponse.StatusCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Create_WhenPostItem_ThenReturnedTheSameItemAndDeletingIt()
        {
            // Arrange
            var item = Initializer.AeroplaneTypeFaker.Generate();

            var testDto = new AeroplaneTypeDto
            {
                Id = item.Id,
                Carrying = item.Carrying,
                Model = item.Model,
                Places = item.Places
            };

            HttpResponseMessage creatingResponse;
            var jsonRequest = JsonConvert.SerializeObject(testDto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // Act
            using (var client = new HttpClient())
            {
                creatingResponse = client.PostAsync($"{serverName}/api/AeroplaneTypes/", content).Result;
            }

            string jsonResponse = creatingResponse.Content.ReadAsStringAsync().Result;
            var resultItem = JsonConvert.DeserializeObject<AeroplaneTypeDto>(jsonResponse);

            // Assert
            Assert.IsNotNull(resultItem);
            Assert.AreEqual(creatingResponse.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public void Delete_WhenDeleteItemById_ThenReturnedStatusCode204()
        {
            // Act
            HttpResponseMessage response;

            using (var client = new HttpClient())
            {
                response = client.DeleteAsync($"{serverName}/api/AeroplaneTypes/{testDto.Id}").Result;
            }
            
            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }


        [TearDown]
        public void Cleanup()
        {
            db.AeroplaneTypes.Remove(addedItem);
        }
    }
}

