using Cloudcustomers.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Threading.Tasks;
using Moq;
using Cloudcustomers.API.Models;
using CloudCustomers.UnitTests.Fixtures;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnStatusCode200(){
        //Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService.Setup(service => service.getAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mockUsersService.Object);
        //Act
        var result =(OkObjectResult) await sut.Get();
        //Assert
        result.StatusCode.Should().Be(200);
        
    }
    [Fact]
    public async Task Get_OnSuccess_InvokeUserServiceExactlyOnce(){
        //Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService.Setup(service => service.getAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUsersService.Object);
        //Act
      var result =await sut.Get();
        //Assert
        mockUsersService.Verify(
            service =>service.getAllUsers(),
            Times.Once()
       );

    }
    [Fact]
    public async Task Get_OnSuccess_ReturnListOfUsers() {
        //Arrange
        var mockUsersService = new Mock<IUsersService>();

        mockUsersService
            .Setup(service => service.getAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mockUsersService.Object);
        //Act
        var result = await sut.Get();
        //Assert
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<User>>();

    }
    [Fact]
    public async Task Get_OnNoUsersFound_Return404()
    {
        //Arrange
        var mockUsersService = new Mock<IUsersService>();

        mockUsersService
            .Setup(service => service.getAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUsersService.Object);
        //Act
        var result = await sut.Get();
        //Assert
        result.Should().BeOfType<NotFoundResult>();
        var objectResult=(NotFoundResult)result;
        objectResult.StatusCode.Should().Be(404);
  

    }
 
}