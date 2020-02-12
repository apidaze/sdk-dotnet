using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Apidaze.SDK.CdrHttpHandlers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using static Apidaze.SDK.Tests.Unit.TestUtil;
using CdrHttpHandlersAlias = Apidaze.SDK.CdrHttpHandlers.CdrHttpHandlers;


namespace Apidaze.SDK.Tests.Unit.CdrHttpHandlers
{
    [TestClass]
    public class CdrHttpHandlersTest : BaseTest
    {
        /// <summary>
        /// The CDR HTTP handler
        /// </summary>
        private CdrHttpHandlersAlias _cdrHttpHandler;

        /// <summary>
        /// Startups this instance.
        /// </summary>
        [TestInitialize]
        public void Startup()
        {
            _cdrHttpHandler = new CdrHttpHandlersAlias(MockIRestClient.Object, CredentialsForTest);
        }

        /// <summary>
        /// Defines the test method GetCdrHttpHandlers_ListOfCdrHttpHandlers_ReturnsCdrHttpHandlers.
        /// </summary>
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

        /// <summary>
        /// Defines the test method CreateCdrHttpHandler_NameAndUri_ReturnsNewCdrHttpHandler.
        /// </summary>
        [TestMethod]
        public void CreateCdrHttpHandler_NameAndUri_ReturnsNewCdrHttpHandler()
        {
            // Arrange
            var cdrHttpHandlers = BuildCdrHttpHandlers();
            var name = cdrHttpHandlers.First().Name;
            var uri = cdrHttpHandlers.First().Url;
            MockIRestClient.Setup(rc => rc.Execute<CdrHttpHandler>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<CdrHttpHandler> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(cdrHttpHandlers.First()) });

            // Act
            var result = _cdrHttpHandler.CreateCdrHttpHandler(name, uri);

            // Assert
            cdrHttpHandlers.First().Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<CdrHttpHandler>(It.IsAny<RestRequest>()), Times.Once);
        }

        /// <summary>
        /// Defines the test method UpdateCdrHttpHandler_IdAndNameAndUri_ReturnsUpdatedCdrHttpHandler.
        /// </summary>
        [TestMethod]
        public void UpdateCdrHttpHandler_IdAndNameAndUri_ReturnsUpdatedCdrHttpHandler()
        {
            // Arrange
            var cdrHttpHandlers = BuildCdrHttpHandlers();
            var id = cdrHttpHandlers.First().Id;
            var name = cdrHttpHandlers.First().Name;
            var uri = cdrHttpHandlers.First().Url;
            MockIRestClient.Setup(rc => rc.Execute<CdrHttpHandler>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<CdrHttpHandler> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(cdrHttpHandlers.First()) });

            // Act
            var result = _cdrHttpHandler.UpdateCdrHttpHandler(id, name, uri);

            // Assert
            cdrHttpHandlers.First().Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<CdrHttpHandler>(It.IsAny<RestRequest>()), Times.Once);
        }

        /// <summary>
        /// Defines the test method UpdateCdrHttpHandlerName_IdAndName_ReturnsUpdatedCdrHttpHandler.
        /// </summary>
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

        /// <summary>
        /// Defines the test method UpdateCdrHttpHandlerUrl_IdAndUrl_ReturnsUpdatedCdrHttpHandler.
        /// </summary>
        [TestMethod]
        public void UpdateCdrHttpHandlerUrl_IdAndUrl_ReturnsUpdatedCdrHttpHandler()
        {
            // Arrange
            var cdrHttpHandlers = BuildCdrHttpHandlers();
            var id = cdrHttpHandlers.First().Id;
            var uri = cdrHttpHandlers.First().Url;
            MockIRestClient.Setup(rc => rc.Execute<CdrHttpHandler>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<CdrHttpHandler> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(cdrHttpHandlers.First()) });

            // Act
            var result = _cdrHttpHandler.UpdateCdrHttpHandlerUrl(id, uri);

            // Assert
            cdrHttpHandlers.First().Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<CdrHttpHandler>(It.IsAny<RestRequest>()), Times.Once);
        }

        /// <summary>
        /// Defines the test method DeleteCdrHttpHandler_Id_NoContent.
        /// </summary>
        [TestMethod]
        public void DeleteCdrHttpHandler_Id_NoContent()
        {
            // Arrange
            const int id = 123;
            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse { StatusCode = HttpStatusCode.NoContent });

            // Act
            _cdrHttpHandler.DeleteCdrHttpHandler(id);

            // Assert
            MockIRestClient.Verify(x => x.Execute(It.IsAny<RestRequest>()), Times.Once);
        }

        /// <summary>
        /// Builds the CDR HTTP handlers.
        /// </summary>
        /// <returns>List&lt;CdrHttpHandler&gt;.</returns>
        private static List<CdrHttpHandler> BuildCdrHttpHandlers()
        {
            return new List<CdrHttpHandler>
            {
                new CdrHttpHandler { Name = "CdrHttpHandler - 0", Url = new Uri("http://url-" + 0 + ".com"), Format = Format.Xml},
                new CdrHttpHandler { Name = "CdrHttpHandler - 1", Url = new Uri("http://url-" + 1 + ".com"), Format = Format.Json}
            };
        }
    }
}
