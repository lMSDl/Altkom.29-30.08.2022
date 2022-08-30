using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppi.Controllers;

namespace WebApi.Test.xUnit.Controllers
{
    public class PeopleControllerTest
    {
        [Fact]
        public async Task Get_ReturnsOkWithAllUsers()
        {
            //Arrange
            var service = new Mock<ICrudService<User>>();

            var expectedList = new Fixture().CreateMany<User>();
            service.Setup(x => x.ReadAsync())
                   .ReturnsAsync(expectedList);

            var controller = new UsersController(service.Object);

            //Act
            var result = await controller.Get();

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var resultList = Assert.IsAssignableFrom<IEnumerable<User>>(actionResult.Value);
            Assert.Equal(expectedList, resultList);
        }

        [Fact]
        public async Task Get_PassId_ReturnsOkSelectedUser()
        {
            //Arrange
            var service = new Mock<ICrudService<User>>();

            var expectedUser = new Fixture().Create<User>();
            service.Setup(x => x.ReadAsync(expectedUser.Id))
                   .ReturnsAsync(expectedUser);

            var controller = new UsersController(service.Object);

            //Act
            var result = await controller.Get(expectedUser.Id);

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var resultUser = Assert.IsAssignableFrom<User>(actionResult.Value);
            Assert.Equal(expectedUser, resultUser);
        }

        //[Fact]
        //public async Task Get_PassId_ReturnsNotFound()
        //{
        //    //Arrange
        //    var service = new Mock<ICrudService<User>>();

        //    service.Setup(x => x.ReadAsync(It.IsAny<int>()))
        //           .ReturnsAsync((User)null!)
        //           .Verifiable();

        //    var controller = new UsersController(service.Object);

        //    //Act
        //    var result = await controller.Get(0);

        //    //Assert
        //    var actionResult = Assert.IsType<NotFoundResult>(result);
        //    service.Verify();
        //}

        [Fact]
        public async Task Get_PassId_ReturnsNotFound()
        {
            await ReturnsNotFound(x => x.Get(0));
        }

        private async Task ReturnsNotFound(Func<UsersController, Task<IActionResult>> func)
        {
            //Arrange
            var service = new Mock<ICrudService<User>>();

            service.Setup(x => x.ReadAsync(It.IsAny<int>()))
                   .ReturnsAsync((User)null!)
                   .Verifiable();

            var controller = new UsersController(service.Object);

            //Act
            var result = await func(controller);

            //Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
            service.Verify();
        }

        [Fact]
        public async Task Delete_ReturnsNotFound()
        {
            await ReturnsNotFound(x => x.Delete(0));
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            //Arrange
            var service = new Mock<ICrudService<User>>();
            var expectedUser = new Fixture().Create<User>();
            service.Setup(x => x.ReadAsync(expectedUser.Id))
                   .ReturnsAsync(expectedUser);
            service.Setup(x => x.DeleteAsync(expectedUser.Id))
                   .Returns(Task.CompletedTask)
                   .Verifiable();

            var controller = new UsersController(service.Object);

            //Act
            var result = await controller.Delete(expectedUser.Id);

            //Assert
            Assert.IsType<NoContentResult>(result);
            service.Verify();
        }

        [Fact]
        public async Task Put_ReturnsNotFound()
        {
            await ReturnsNotFound(x => x.Put(0, null!));
        }
    }
}
