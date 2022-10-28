using Cloudcustomers.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using CloudCustomers.UnitTests.Fixtures;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class TestDronesController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnStatusCode200()
    {
        //Arrange
        var mockDronesService = new Mock<IDronesService>();
        mockDronesService.Setup(service => service.getAllDrones())
            .ReturnsAsync(DroneFixture.GetTestDrones());
        var sut = new DroneController(mockDronesService.Object);
        //Act
        var result = (OkObjectResult)await sut.Get();
        //Assert
        result.StatusCode.Should().Be(200);

    }
    [Fact]
    public async Task Get_OnSuccess_InvokeDronesServiceExactlyOnce()
    {
        //Arrange
        var mockDronesService = new Mock<IDronesService>();
        mockDronesService.Setup(service => service.getAllDrones())
           .ReturnsAsync(DroneFixture.GetTestDrones());
        var sut = new DroneController(mockDronesService.Object);
        //Act
        var result = await sut.Get();
        //Assert
        mockDronesService.Verify(
            service => service.getAllDrones(),
            Times.Once()
       );

    }
}

