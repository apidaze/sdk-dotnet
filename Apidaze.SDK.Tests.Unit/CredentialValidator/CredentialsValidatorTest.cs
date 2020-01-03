using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Apidaze.SDK.Base;
using Apidaze.SDK.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockServerClientNet;
using MockServerClientNet.Model;
using Moq;
using Moq.Protected;
using RestSharp;
using HttpResponse = MockServerClientNet.Model.HttpResponse;
using Times = MockServerClientNet.Model.Times;

namespace Apidaze.SDK.Tests.Unit.CredentialValidator
{
    [TestClass]
    public class CredentialsValidatorTest
    {

        private static string MockServerHost = "http://localhost";
        private MockServerClient _mockServerClient;
        private static readonly string _apiKey = "some-api-key";
        private static readonly string _apiSecret = "some-api-secret";
        private CredentialsValidator.CredentialsValidator _validator;
        private Mock<IRestClient> _mockIRestClient;

        [TestInitialize]
        public void Startup()
        {
            _mockIRestClient = new Mock<IRestClient>();

            _validator = CredentialsValidator.CredentialsValidator.Create(_mockIRestClient.Object, new Credentials(_apiKey, _apiSecret), "127.0.0.1:1080");
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod]
        public void ValidateCredentials_StatusCodeOK_ReturnTrue()
        {
            // Arrange
            var responseBody = "\"status\": { \"global\": \"Authentication succeeded\" }";

            Mock<IRestResponse> restResponseMock = new Mock<IRestResponse>();

            restResponseMock.Setup(x => x.StatusCode).Returns(HttpStatusCode.OK);
            _mockIRestClient.Setup(m => m.Execute(It.IsAny<RestRequest>())).Returns(restResponseMock.Object);

            // Act 
            var result = _validator.ValidateCredentials();

            // Assert
            // restClient.Verify(ValidateCredentialsRequest());
            Assert.IsTrue(result);
        }

        private static HttpRequest ValidateCredentialsRequest()
        {
            return HttpRequest.Request().WithMethod("GET")
                .WithPath("/" + _apiKey + "/validates")
                .WithQueryStringParameters(new MockServerClientNet.Model.Parameter("api_secret", _apiSecret));

        }
    }
}
