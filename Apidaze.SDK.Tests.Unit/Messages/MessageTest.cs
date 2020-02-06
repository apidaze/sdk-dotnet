using Apidaze.SDK.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Net;
using static Apidaze.SDK.Tests.Unit.TestUtil;

namespace Apidaze.SDK.Tests.Unit.Messages
{
    [TestClass]
    public class MessageTest : BaseTest
    {
        private Message _messageApi;

        [TestInitialize]
        public void Startup()
        {
            _messageApi = new Message(MockIRestClient.Object, CredentialsForTest);
        }

        [TestMethod]
        public void SendTextMessage_WithPhoneNumbersAndMessage_ResponseBodyOK()
        {
            // Arrange
            var responseBody = "{\"ok\":true,\"message\":\"SMS sent\"}";
            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
               new RestResponse { StatusCode = HttpStatusCode.OK, Content = responseBody });

            // Act
            var result = _messageApi.SendTextMessage(new PhoneNumber("123456789"), new PhoneNumber("123456789"), "Hello");

            // Assert
            Assert.AreEqual(result, responseBody);
        }

        [TestMethod]
        public void SendTextMessage_WithPhoneNumbersAndNoMessage_ArgumentExceptionThrowed()
        {
            // Arrange
            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse { StatusCode = HttpStatusCode.OK });

            // Act + Assert
            Assert.ThrowsException<ArgumentException>(() => _messageApi.SendTextMessage(new PhoneNumber("123456789"), new PhoneNumber("123456789"), null));
        }

        [TestMethod]
        public void SendTextMessage_WithPhoneNumbersAndMessage_ResponseServerError()
        {
            // Arrange
            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>()))
                .Returns(new RestResponse { StatusCode = HttpStatusCode.InternalServerError });

            // Act + Assert
            Assert.ThrowsException<InvalidOperationException>(() => _messageApi.SendTextMessage(new PhoneNumber("123456789"), new PhoneNumber("123456789"), "Hello"));
        }
    }
}