using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using APIdaze.SDK.Base;
using APIdaze.SDK.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockServerClientNet;
using MockServerClientNet.Model;
using Moq;
using Moq.Protected;
using RestSharp;
using HttpResponse = MockServerClientNet.Model.HttpResponse;
using Times = MockServerClientNet.Model.Times;
using static APIdaze.SDK.Tests.Unit.TestUtil;


namespace APIdaze.SDK.Tests.Unit.CredentialValidator
{
    [TestClass]
    public class CredentialsValidatorTest : BaseTest
    {
        private CredentialsValidator.CredentialsValidator _validator;
        
        [TestInitialize]
        public void Startup()
        {
            _validator = new CredentialsValidator.CredentialsValidator(mockIRestClient.Object, CREDENTIALS);
        }

        [TestMethod]
        public void ValidateCredentials_StatusCodeOK_ReturnTrue()
        {
            // Arrange
            Mock<IRestResponse> restResponseMock = new Mock<IRestResponse>();
            restResponseMock.Setup(x => x.StatusCode).Returns(HttpStatusCode.OK);
            mockIRestClient.Setup(m => m.Execute(It.IsAny<RestRequest>())).Returns(restResponseMock.Object);

            // Act 
            var result = _validator.ValidateCredentials();

            // Assert
            Assert.IsTrue(result);
        }

    }
}
