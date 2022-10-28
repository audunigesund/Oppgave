using Cloudcustomers.API.Models;
using Cloudcustomers.API.Dtos;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Moq;
using Moq.Protected;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService{
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient =new HttpClient(handlerMock.Object);
            var sut= new UsersService(httpClient);
            //Act
            await sut.getAllUsers();
            //Assert
            handlerMock
                .Protected()
                .Verify(
                "SendAsync", 
                Times.Exactly(1), 
                ItExpr.Is<HttpRequestMessage>(req => req.Method==HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );

        }
        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnListOfUsers()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var sut = new UsersService(httpClient);
            //Act
            var result = await sut.getAllUsers();
            //Assert
            result.Should().BeOfType<List<User>>();

        }
       /*[Fact]
        public async Task GetAllDrones_WhenCalled_ReturnListOfDrones()
        {
            //Arrange
            var expectedResponse = DroneFixture.GetTestDrones();
            var handlerMock = MockHttpMessageHandler<DroneDto>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var sut = new UsersService(httpClient);
            //Act
            var result = await sut.getAllDrones();
            //Assert
            result.Should().BeOfType<List<DroneDto>>();

        }*/
    }
}
