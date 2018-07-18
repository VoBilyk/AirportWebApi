using System;
using NUnit.Framework;
using FakeItEasy;
using Airport.BLL.Interfaces;
using System.Net;
using Airport.Shared.DTO;
using Airport.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Airport.Tests.Controllers
{
    [TestFixture]
    public class PilotsControllerTest
    {
        [Test]
        public void GET_WhenGetItem_ThenServiceReturnOkAndThisObject()
        {
            //Arrange
            var id = Guid.NewGuid();

            var fakeService = A.Fake<IPilotService>();
            A.CallTo(() => fakeService.Get(id)).Returns(new PilotDto());

            var controller = new PilotsController(fakeService);

            //Act
            var response = controller.Get(id) as ObjectResult;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.IsInstanceOf(typeof(PilotDto), response.Value);
        }

        [Test]
        public void POST_WhenPostNewItem_ThenServiceReturnOkAndThisObject()
        {
            //Arrange
            var pilot = new PilotDto
            {
                Id = Guid.NewGuid(),
                FirstName = "FirstName",
                SecondName = "SecondName",
                Experience = 4,
                BirthDate = new DateTime(1980, 1, 1)
            };

            var fakeService = A.Fake<IPilotService>();
            A.CallTo(() => fakeService.Create(pilot)).Returns(pilot);
            
            var controller = new PilotsController(fakeService);

            //Act
            var response = controller.Post(pilot) as ObjectResult;
            
            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.IsInstanceOf(typeof(PilotDto), response.Value);
        }

        [Test]
        public void PUT_WhenPuttNewItem_ThenServiceReturnOkAndThisObject()
        {
            //Arrange
            var pilot = new PilotDto
            {
                Id = Guid.NewGuid(),
                FirstName = "FirstName",
                SecondName = "SecondName",
                Experience = 4,
                BirthDate = new DateTime(1980, 1, 1)
            };

            var fakeService = A.Fake<IPilotService>();
            A.CallTo(() => fakeService.Update(pilot.Id, pilot)).Returns(pilot);

            var controller = new PilotsController(fakeService);

            //Act
            var response = controller.Post(pilot) as ObjectResult;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.IsInstanceOf(typeof(PilotDto), response.Value);
        }

        [Test]
        public void DELETE_WhenDeleteItem_ThenServiceReturnNoContent()
        {
            //Arrange
            var id = Guid.NewGuid();

            var fakeService = A.Fake<IPilotService>();

            var controller = new PilotsController(fakeService);

            //Act
            var response = controller.Delete(id) as NoContentResult;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
