using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using APIdaze.SDK.Base;
using APIdaze.SDK.Messages;
using Newtonsoft.Json.Linq;
using static APIdaze.SDK.Tests.Unit.TestUtil;

namespace APIdaze.SDK.Tests.Unit
{
    [TestClass]
    public class MessageTest : BaseTest
    {
        [TestMethod]
        public void CreateMessage_WithPhoneNumbersAndMessage_ResponseBodyOK()
        {
            // Arrange
            var responseBody = "{\"ok\":true,\"message\":\"SMS sent\"}";
            mockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
               new RestResponse { StatusCode = HttpStatusCode.Accepted , Content = responseBody});

            var messageApi = new MessageApi(mockIRestClient.Object, CREDENTIALS );

            // Act
            var result = messageApi.SendTextMessage(new PhoneNumber("123456789"), new PhoneNumber("123456789"), "Hello");

            // Assert
            Assert.AreEqual(result, responseBody);
        }

        [TestMethod]
        public void CreateMessage_WithPhoneNumbersAndNoMessage_ArgumentExceptionThrowed()
        {
            // Arrange
            mockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse { StatusCode = HttpStatusCode.Accepted });
            var messageApi = new MessageApi(mockIRestClient.Object, CREDENTIALS);

            // Act + Assert
            Assert.ThrowsException<ArgumentException>(() => messageApi.SendTextMessage(new PhoneNumber("123456789"), new PhoneNumber("123456789"), null));
        }

        [TestMethod]
        public void CreateMessage_WithPhoneNumbersAndMessage_ResponseServerError()
        {
            // Arrange
            var responseBody = "{\"ok\":true,\"message\":\"SMS sent\"}";
            mockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse { StatusCode = HttpStatusCode.InternalServerError, Content = responseBody });

            var messageApi = new MessageApi(mockIRestClient.Object, CREDENTIALS);

            // Act + Assert
            Assert.ThrowsException<InvalidOperationException>(() => messageApi.SendTextMessage(new PhoneNumber("123456789"), new PhoneNumber("123456789"), "Hello"));
        }
    }
}
