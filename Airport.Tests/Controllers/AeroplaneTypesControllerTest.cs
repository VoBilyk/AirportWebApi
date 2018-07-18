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
    public class AeroplaneTypesControllerTest
    {
        [Test]
        public void GET_WhenGetItem_ThenServiceReturnOkAndThisObject()
        {
            //Arrange
            var id = Guid.NewGuid();

            var fakeService = A.Fake<IAeroplaneTypeService>();
            A.CallTo(() => fakeService.Get(id)).Returns(new AeroplaneTypeDto());

            var controller = new AeroplaneTypesController(fakeService);

            //Act
            var response = controller.Get(id) as ObjectResult;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.IsInstanceOf(typeof(AeroplaneTypeDto), response.Value);
        }

        [Test]
        public void POST_WhenPostNewItem_ThenServiceReturnOkAndThisObject()
        {
            //Arrange
            var AeroplaneType = new AeroplaneTypeDto
            {
                Id = Guid.NewGuid(),
                Model = "Model",
                Carrying = 100000,
                Places = 50
            };

            var fakeService = A.Fake<IAeroplaneTypeService>();
            A.CallTo(() => fakeService.Create(AeroplaneType)).Returns(AeroplaneType);

            var controller = new AeroplaneTypesController(fakeService);

            //Act
            var response = controller.Post(AeroplaneType) as ObjectResult;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.IsInstanceOf(typeof(AeroplaneTypeDto), response.Value);
        }

        [Test]
        public void PUT_WhenPuttNewItem_ThenServiceReturnOkAndThisObject()
        {
            //Arrange
            var AeroplaneType = new AeroplaneTypeDto
            {
                Id = Guid.NewGuid(),
                Model = "Model",
                Carrying = 100000,
                Places = 50
            };

            var fakeService = A.Fake<IAeroplaneTypeService>();
            A.CallTo(() => fakeService.Update(AeroplaneType.Id, AeroplaneType)).Returns(AeroplaneType);

            var controller = new AeroplaneTypesController(fakeService);

            //Act
            var response = controller.Post(AeroplaneType) as ObjectResult;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.IsInstanceOf(typeof(AeroplaneTypeDto), response.Value);
        }

        [Test]
        public void DELETE_WhenDeleteItem_ThenServiceReturnNoContent()
        {
            //Arrange
            var id = Guid.NewGuid();

            var fakeService = A.Fake<IAeroplaneTypeService>();

            var controller = new AeroplaneTypesController(fakeService);

            //Act
            var response = controller.Delete(id) as NoContentResult;

            //Assert
            Assert.AreEqual((int)HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
