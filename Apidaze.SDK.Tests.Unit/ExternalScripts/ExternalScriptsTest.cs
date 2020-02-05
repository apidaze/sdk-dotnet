using Apidaze.SDK.ExternalScripts;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using static Apidaze.SDK.Tests.Unit.TestUtil;

namespace Apidaze.SDK.Tests.Unit.ExternalScripts
{
    [TestClass]
    public class ExternalScriptsTest : BaseTest
    {
        private SDK.ExternalScripts.ExternalScripts _externalScripts;

        [TestInitialize]
        public void Startup()
        {
            _externalScripts = new SDK.ExternalScripts.ExternalScripts(MockIRestClient.Object, CredentialsForTest);
        }

        [TestMethod]
        public void GetExternalScripts_ListOfExternalScripts_ReturnsScriptsList()
        {
            // Arrange
            var scripts = BuildExternalScriptsLists();
            MockIRestClient.Setup(rc => rc.Execute<List<ExternalScript>>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<List<ExternalScript>> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(scripts) });

            // Act
            var result = _externalScripts.GetExternalScripts();

            // Assert
            scripts.Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<List<ExternalScript>>(It.IsAny<RestRequest>()), Times.Once);
        }

        [TestMethod]
        public void GetExternalScriptById_ExternalScript_ReturnsOneScript()
        {
            // Arrange
            var scripts = BuildExternalScriptsLists();
            var id = scripts.First().Id;
            MockIRestClient.Setup(rc => rc.Execute<ExternalScript>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<ExternalScript> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(scripts.First()) });

            // Act
            var result = _externalScripts.GetExternalScript(id);

            // Assert
            scripts.First().Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<ExternalScript>(It.IsAny<RestRequest>()), Times.Once);
        }

        [TestMethod]
        public void GetExternalScriptById_NotFound_ReturnsEmpty()
        {
            // Arrange
            var id = 1L;
            MockIRestClient.Setup(rc => rc.Execute<ExternalScript>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<ExternalScript> { StatusCode = HttpStatusCode.NotFound });

            // Act
            var result = _externalScripts.GetExternalScript(id);

            // Assert
            Assert.IsNull(result);
            MockIRestClient.Verify(x => x.Execute<ExternalScript>(It.IsAny<RestRequest>()), Times.Once);
        }

        [TestMethod]
        public void UpdateExternalScriptUrl_IdAndUrl_ScriptUpdated()
        {
            // Arrange
            var scripts = BuildExternalScriptsLists();
            var id = scripts.First().Id;
            var scriptUrl = scripts.First().Url;

            MockIRestClient.Setup(rc => rc.Execute<ExternalScript>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<ExternalScript> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(scripts.First()) });

            // Act
            var result = _externalScripts.UpdateExternalScriptUrl(id, scriptUrl);

            // Assert
            scripts.First().Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<ExternalScript>(It.IsAny<RestRequest>()), Times.Once);
        }

        [TestMethod]
        public void UpdateExternalScriptUrl_IdAndUrlAndName_ScriptUpdated()
        {
            // Arrange
            var scripts = BuildExternalScriptsLists();
            var id = scripts.First().Id;
            var name = scripts.First().Name;
            var scriptUrl = scripts.First().Url;

            MockIRestClient.Setup(rc => rc.Execute<ExternalScript>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<ExternalScript> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(scripts.First()) });

            // Act
            var result = _externalScripts.UpdateExternalScript(id, name, scriptUrl);

            // Assert
            scripts.First().Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<ExternalScript>(It.IsAny<RestRequest>()), Times.Once);
        }

        [TestMethod]
        public void UpdateExternalScriptUrl_NameIsTooLong_ApiNotInvokedArgumentException()
        {
            // Arrange
            var scripts = BuildExternalScriptsLists();
            var id = scripts.First().Id;
            var name = scripts.First().Name;
            var scriptUrl = scripts.First().Url;

            MockIRestClient.Setup(rc => rc.Execute<ExternalScript>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<ExternalScript> { StatusCode = HttpStatusCode.InternalServerError });

            // Act + assert
            Assert.ThrowsException<InvalidOperationException>(() => _externalScripts.UpdateExternalScript(id, name, scriptUrl));
            MockIRestClient.Verify(x => x.Execute<ExternalScript>(It.IsAny<RestRequest>()), Times.Once);
        }

        [TestMethod]
        public void UpdateExternalScriptUrl_ErrorApi_InvalidOperationException()
        {
            // Arrange
            var scripts = BuildExternalScriptsLists();
            var id = scripts.First().Id;
            var name = "Very long name.................................";
            var scriptUrl = scripts.First().Url;

            MockIRestClient.Setup(rc => rc.Execute<ExternalScript>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<ExternalScript> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(scripts.First()) });

            // Act + assert
            Assert.ThrowsException<ArgumentException>(() => _externalScripts.UpdateExternalScript(id, name, scriptUrl));
            MockIRestClient.Verify(x => x.Execute<ExternalScript>(It.IsAny<RestRequest>()), Times.Never);
        }

        private List<ExternalScript> BuildExternalScriptsLists()
        {
            return new List<ExternalScript> {
                new ExternalScript {
                    Id = 1L,
                    Name = "1st script",
                    Url = new Uri("https://my.first.application.com"),
                    SmsUrl = new Uri("https://my.first.sms.application.com"),
                    ResellerCustomerId = 5L,
                    DevCustomerId = 6L,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now },
                new ExternalScript {
                    Id = 2L,
                    Name = "2nd script",
                    Url = new Uri("https://my.second.application.com"),
                    SmsUrl = new Uri("https://my.second.sms.application.com"),
                    ResellerCustomerId = 15L,
                    DevCustomerId =16L,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now } };
        }
    }
}