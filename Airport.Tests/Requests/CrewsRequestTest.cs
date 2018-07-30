using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Airport.DAL.Entities;
using Airport.Shared.DTO;
using Airport.DAL;


namespace Airport.Tests.Requests
{
    [TestFixture]
    public class CrewsRequestTest
    {
        private string serverName;
        private AirportContext db;
        private CrewDto testDto;
        private Crew addedItem;


        [SetUp]
        public void Setup()
        {
            serverName = "http://localhost:57338";

            var builder = new DbContextOptionsBuilder<AirportContext>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AirportDb(Test);Trusted_Connection=True;");

            addedItem = Initializer.CrewFaker.Generate();

            testDto = new CrewDto
            {
                Id = addedItem.Id,
                PilotId = addedItem.Pilot.Id,
                StewardessesId = addedItem.Stewardesses.Select(x => x.Id).ToList()
            };

            db = new AirportContext(builder.Options);

            // Adding test item to db
            db.Crews.Add(addedItem);
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
                response = client.GetAsync(serverName + "/api/Crews").Result;
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
                response = client.GetAsync($"{serverName}/api/Crews/{addedItem.Id}").Result;
            }

            string contextResponse = response.Content.ReadAsStringAsync().Result;
            var resultItem = JsonConvert.DeserializeObject<CrewDto>(contextResponse);

            // Assert
            Assert.IsNotNull(resultItem);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(addedItem.Id, resultItem.Id);
        }

        [Test]
        public void Create_WhenPostEmptyItem_ThenReturnedBadRequestStatusCode()
        {
            // Arrange
            var testDto = new CrewDto { };

            HttpResponseMessage creatingResponse;
            var jsonRequest = JsonConvert.SerializeObject(testDto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // Act
            using (var client = new HttpClient())
            {
                creatingResponse = client.PostAsync($"{serverName}/api/Crews/", content).Result;
            }

            string jsonResponse = creatingResponse.Content.ReadAsStringAsync().Result;
            var resultItem = JsonConvert.DeserializeObject<CrewDto>(jsonResponse);

            // Assert
            Assert.AreEqual(creatingResponse.StatusCode, HttpStatusCode.BadRequest);
        }


        [TearDown]
        public void Cleanup()
        {
            db.Crews.Remove(addedItem);
        }
    }
}

