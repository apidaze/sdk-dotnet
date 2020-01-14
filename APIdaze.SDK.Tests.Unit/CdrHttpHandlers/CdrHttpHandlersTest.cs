using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using APIdaze.SDK.CdrHttpHandlers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using static APIdaze.SDK.Tests.Unit.TestUtil;
using CdrHttpHandlersAlias = APIdaze.SDK.CdrHttpHandlers.CdrHttpHandlers;


namespace APIdaze.SDK.Tests.Unit.CdrHttpHandlers
{
    [TestClass]
    public class CdrHttpHandlersTest : BaseTest
    {
        private CdrHttpHandlersAlias _cdrHttpHandler;

        [TestInitialize]
        public void Startup()
        {
            _cdrHttpHandler = new CdrHttpHandlersAlias(MockIRestClient.Object, Credentials);
        }

        [TestMethod]
        public void GetCdrHttpHandlers_ListOfCdrHttpHandlers_ReturnsCdrHttpHandlers()
        {
            // Arrange
            var cdrHttpHandlers = BuildCdrHttpHandlers();
            MockIRestClient.Setup(rc => rc.Execute<List<CdrHttpHandler>>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<List<CdrHttpHandler>> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(cdrHttpHandlers) });

            // Act
            var result = _cdrHttpHandler.GetCdrHttpHandlers();

            // Assert
            cdrHttpHandlers.Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<List<CdrHttpHandler>>(It.IsAny<RestRequest>()), Times.Once);
        }

        [TestMethod]
        public void CreateCdrHttpHandler_NameAndUri_ReturnsNewCdrHttpHandler()
        {
            // Arrange
            var cdrHttpHandlers = BuildCdrHttpHandlers();
            var name = cdrHttpHandlers.First().Name;
            var uri = cdrHttpHandlers.First().Uri;
            MockIRestClient.Setup(rc => rc.Execute<CdrHttpHandler>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<CdrHttpHandler> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(cdrHttpHandlers.First()) });

            // Act
            var result = _cdrHttpHandler.CreateCdrHttpHandler(name, uri);

            // Assert
            cdrHttpHandlers.First().Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<CdrHttpHandler>(It.IsAny<RestRequest>()), Times.Once);
        }

        [TestMethod]
        public void UpdateCdrHttpHandler_IdAndNameAndUri_ReturnsUpdatedCdrHttpHandler()
        {
            // Arrange
            var cdrHttpHandlers = BuildCdrHttpHandlers();
            var id = cdrHttpHandlers.First().Id;
            var name = cdrHttpHandlers.First().Name;
            var uri = cdrHttpHandlers.First().Uri;
            MockIRestClient.Setup(rc => rc.Execute<CdrHttpHandler>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<CdrHttpHandler> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(cdrHttpHandlers.First()) });

            // Act
            var result = _cdrHttpHandler.UpdateCdrHttpHandler(id, name, uri);

            // Assert
            cdrHttpHandlers.First().Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<CdrHttpHandler>(It.IsAny<RestRequest>()), Times.Once);
        }

        [TestMethod]
        public void UpdateCdrHttpHandlerName_IdAndName_ReturnsUpdatedCdrHttpHandler()
        {
            // Arrange
            var cdrHttpHandlers = BuildCdrHttpHandlers();
            var id = cdrHttpHandlers.First().Id;
            var name = cdrHttpHandlers.First().Name;
            MockIRestClient.Setup(rc => rc.Execute<CdrHttpHandler>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<CdrHttpHandler> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(cdrHttpHandlers.First()) });

            // Act
            var result = _cdrHttpHandler.UpdateCdrHttpHandlerName(id, name);

            // Assert
            cdrHttpHandlers.First().Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<CdrHttpHandler>(It.IsAny<RestRequest>()), Times.Once);
        }

        [TestMethod]
        public void UpdateCdrHttpHandlerUrl_IdAndUrl_ReturnsUpdatedCdrHttpHandler()
        {
            // Arrange
            var cdrHttpHandlers = BuildCdrHttpHandlers();
            var id = cdrHttpHandlers.First().Id;
            var uri = cdrHttpHandlers.First().Uri;
            MockIRestClient.Setup(rc => rc.Execute<CdrHttpHandler>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<CdrHttpHandler> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(cdrHttpHandlers.First()) });

            // Act
            var result = _cdrHttpHandler.UpdateCdrHttpHandlerUrl(id, uri);

            // Assert
            cdrHttpHandlers.First().Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<CdrHttpHandler>(It.IsAny<RestRequest>()), Times.Once);
        }

        [TestMethod]
        public void DeleteCdrHttpHandler_Id_NoContent()
        {
            // Arrange
            const int id = 123;
            MockIRestClient.Setup(rc => rc.Execute<CdrHttpHandler>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<CdrHttpHandler> { StatusCode = HttpStatusCode.NoContent });

            // Act
            _cdrHttpHandler.DeleteCdrHttpHandler(id);

            // Assert
            MockIRestClient.Verify(x => x.Execute<CdrHttpHandler>(It.IsAny<RestRequest>()), Times.Once);
        }

        private static List<CdrHttpHandler> BuildCdrHttpHandlers()
        {
            return new List<CdrHttpHandler>
            {
                new CdrHttpHandler { Name = "CdrHttpHandler - 0", Uri = new Uri("http://url-" + 0 + ".com"), Format = Format.Xml},
                new CdrHttpHandler { Name = "CdrHttpHandler - 1", Uri = new Uri("http://url-" + 1 + ".com"), Format = Format.Json}
            };
        }
    }
}
