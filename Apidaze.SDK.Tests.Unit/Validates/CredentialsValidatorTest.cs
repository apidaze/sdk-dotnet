using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System.Net;
using Apidaze.SDK.Messages;
using Apidaze.SDK.Validates;
using static Apidaze.SDK.Tests.Unit.TestUtil;


namespace Apidaze.SDK.Tests.Unit.Validates
{
    [TestClass]
    public class CredentialsValidatorTest : BaseTest
    {
        private  CredentialsValidator _validator;

        [TestInitialize]
        public void Startup()
        {
            _validator = new CredentialsValidator(MockIRestClient.Object, CredentialsForTest);
        }

        [TestMethod]
        public void ValidateCredentials_StatusCodeOK_ReturnTrue()
        {
            // Arrange
            var responseBody = "\"status\": { \"global\": \"Authentication succeeded\" }";
            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse { StatusCode = HttpStatusCode.OK, Content = responseBody });

            // Act 
            var result = _validator.ValidateCredentials();

            // Assert
            Assert.IsTrue(result);
            MockIRestClient.Verify(x => x.Execute(It.IsAny<RestRequest>()), Times.Once);
        }

        [TestMethod]
        public void ValidateCredentials_StatusCodeUnauthorized_ReturnFalse()
        {
            // Arrange
            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse { StatusCode = HttpStatusCode.Unauthorized });

            // Act 
            var result = _validator.ValidateCredentials();

            // Assert
            Assert.IsFalse(result);
            MockIRestClient.Verify(x => x.Execute(It.IsAny<RestRequest>()), Times.Once);
        }

        [TestMethod]
        public void ValidateCredentials_StatusCodeInternalServerError_ShouldThrowIOException()
        {
            // Arrange
            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse { StatusCode = HttpStatusCode.InternalServerError });

            // Act + Assert
            Assert.ThrowsException<InvalidOperationException>(() => _validator.ValidateCredentials());
        }
    }
}
