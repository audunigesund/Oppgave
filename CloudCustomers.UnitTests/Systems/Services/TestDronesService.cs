using Cloudcustomers.API.Config;
using Cloudcustomers.API.Controllers;
using Cloudcustomers.API.Dtos;    
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestDronesService{
        [Fact]
        public async Task GetAllDrones_WhenCalled_InvokeHttpGetReques()
        {
            //Arrange
            var expectedResponse = DroneFixture.GetTestDrones();
            var endpoint = "http//example.com";
            var handlerMock = MockHttpMessageHandler<DroneDto>
                .SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var config = Options.Create(
                new DronesApiOptions
                {
                    Endpoint = endpoint
                });
            var sut = new DronesService(httpClient,config);
            //Act
            await sut.getAllDrones();
            //Assert
            handlerMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1), 
                ItExpr.Is<HttpRequestMessage>(req =>req.Method ==HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());

        }
        [Fact]
        public async Task GetAllDrones_WhenCalled_ReturnListOfDrones()
        {
            //Arrange
            var expectedResponse = DroneFixture.GetTestDrones();
            var handlerMock = MockHttpMessageHandler<DroneDto>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "http//example.com";
            var config = Options.Create(
                new DronesApiOptions
                {
                    Endpoint = endpoint
                });
            var sut = new DronesService(httpClient, config);
            //Act
            var result = await sut.getAllDrones();
            //Assert
            result.Should().BeOfType<List<DroneDto>>();

        }
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
    }
}
