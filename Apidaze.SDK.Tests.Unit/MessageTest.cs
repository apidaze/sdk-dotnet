using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Apidaze.SDK.Messages;
using Apidaze.SDK.Base;

namespace Apidaze.SDK.Tests.Unit
{
    [TestClass]
    public class MessageTest
    {
        private Mock<IRestClient> _mockIRestClient;

        [TestInitialize]
        public void Startup()
        {
            _mockIRestClient = new Mock<IRestClient>();

        }
        [TestMethod]
        public void CreateMessage_ParametersSent_Result()
        {
            //Given
            string appKey = "341098ad80e99271cb551ff2af05ffa4339ffdbf";
            string secretKey = "29b2158823ed714853680aecb0edd60f5a656074";
            var restClientMock = new Mock<RestClient>();
            var credentials = new Credentials(appKey, secretKey);
            var requestMock = new Mock<RestRequest>();
            restClientMock.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                (RestResponse<string>)new RestResponse { StatusCode = HttpStatusCode.OK });

            var target = Message.Create(_mockIRestClient.Object, credentials);

            //When
            var result = target.SendTextMessage(new PhoneNumber("123456789"), new PhoneNumber("123456789"), "Hello");

            //Then
            Assert.AreEqual(result, "");
        }
    }
}
