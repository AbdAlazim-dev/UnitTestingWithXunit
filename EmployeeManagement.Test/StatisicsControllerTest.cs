
using AutoMapper;
using EmployeeManagement.Controllers;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeManagement.Test
{
    public class StatisicsControllerTest
    {
        [Fact]
        public void GetStatisics_InputFromHttpConnectionFeature_MustBeReturnInputIps()
        {
            // Arrange
            var localIpAddress = System.Net.IPAddress.Parse("111.111.111.111");
            var locaPort = 5000;
            var remoteIpAddress = System.Net.IPAddress.Parse("222.222.222.222");
            var remotePort = 8080;

            var featureCollectionMock = new Mock<IFeatureCollection>();

            featureCollectionMock.Setup(m => m.Get<IHttpConnectionFeature>())
                .Returns(new HttpConnectionFeature()
                {
                    LocalIpAddress = localIpAddress,
                    LocalPort = locaPort,
                    RemoteIpAddress = remoteIpAddress,
                    RemotePort = remotePort
                });

            var httpContextMock = new Mock<HttpContext>();

            httpContextMock.Setup(m => m.Features)
                .Returns(featureCollectionMock.Object);

            var mapperConfiguration = new MapperConfiguration(cnf =>
                cnf.AddProfile<MapperProfiles.StatisticsProfile>());

            var mapper = new Mapper(mapperConfiguration);

            var statisticController = new StatisticsController(mapper);

            statisticController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContextMock.Object,
            };

            // Act
            var statisicsResult = statisticController.GetStatistics();

            //Assert
            var actionResult = Assert
                .IsType<ActionResult<StatisticsDto>>(statisicsResult);

            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            var staticsDto = Assert.IsType<StatisticsDto>(okObjectResult.Value);

            Assert.Equal(remotePort, staticsDto.RemotePort);
            Assert.Equal(localIpAddress.ToString(), staticsDto.LocalIpAddress);
            Assert.Equal(remoteIpAddress.ToString(), staticsDto.RemoteIpAddress);
            Assert.Equal(locaPort, staticsDto.LocalPort);
        }
    }
}
