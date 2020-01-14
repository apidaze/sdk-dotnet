using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using APIdaze.SDK.Calls;
using APIdaze.SDK.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using static APIdaze.SDK.Tests.Unit.TestUtil;

namespace APIdaze.SDK.Tests.Unit.Calls
{
    [TestClass]
    public class CallsTests : BaseTest
    {
        private SDK.Calls.Calls _callsApi;

        [TestInitialize]
        public void Startup()
        {
            _callsApi = new SDK.Calls.Calls(MockIRestClient.Object, Credentials);
        }

        [TestMethod]
        public void CreateCall_NumberCallType_CallId()
        {
            // Arrange
            var callerId = new PhoneNumber("14123456789");
            var origin = "48123456789";
            var destination = "48987654321";
            var callType = CallType.NUMBER;
            var callId = new Guid("d64baf26-b116-4478-97b5-899de580461f");
            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse { StatusCode = HttpStatusCode.Accepted, Content = "{\"ok\" : \"" + callId + "\"}" });

            // Act
            var result = _callsApi.CreateCall(callerId, origin, destination, callType);

            // Assert
            Assert.AreEqual(result, callId);
        }

        [TestMethod]
        public void CreateCall_SipAccountCallType_CallId()
        {
            // Arrange
            var callerId = new PhoneNumber("14123456789");
            var origin = "sip-account-origin";
            var destination = "sip-account-destination";
            var callType = CallType.SIP_ACCOUNT;
            var callId = new Guid("d64baf26-b116-4478-97b5-899de580461f");

            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse { StatusCode = HttpStatusCode.Accepted, Content = "{\"ok\" : \"" + callId + "\"}" });

            // Act
            var result = _callsApi.CreateCall(callerId, origin, destination, callType);

            // Assert
            Assert.AreEqual(result, callId);
        }

        [TestMethod]
        public void CreateCall_FailureMessage_CreateCallResponseExceptionThrowed()
        {
            // Arrange
            var callerId = new PhoneNumber("14123456789");
            var origin = "48123456789";
            var destination = "48987654321";
            var callType = CallType.NUMBER;
            var failureMessage = "NORMAL TEMPORARY_FAILURE";

            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse
                { StatusCode = HttpStatusCode.Accepted, Content = "{\"failure\" : \"" + failureMessage + "\"}" });

            // Act + Assert
            var exception = Assert.ThrowsException<CreateCallResponseException>(() =>
                _callsApi.CreateCall(callerId, origin, destination, callType));
            Assert.AreEqual(failureMessage, exception.Message);
        }

        [TestMethod]
        public void CreateCall_EmptyJson_CreateCallResponseExceptionThrowed()
        {
            // Arrange
            var callerId = new PhoneNumber("14123456789");
            var origin = "48123456789";
            var destination = "48987654321";
            var callType = CallType.NUMBER;
            var failureMessage = "missing call id in the response body";

            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse { StatusCode = HttpStatusCode.Accepted, Content = "{}" });

            // Act + Assert
            var exception = Assert.ThrowsException<CreateCallResponseException>(() =>
                _callsApi.CreateCall(callerId, origin, destination, callType));
            Assert.AreEqual(failureMessage, exception.Message);
        }

        [TestMethod]
        public void DeleteCall_Guid_DeleteExecutedOnce()
        {
            // Arrange
            var callId = new Guid("d64baf26-b116-4478-97b5-899de580461f");

            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse { StatusCode = HttpStatusCode.Accepted, Content = "{\"ok\" : \"\"}" });

            // Act
            _callsApi.DeleteCall(callId);

            // Assert
            MockIRestClient.Verify(x => x.Execute(It.IsAny<RestRequest>()), Times.Once);
        }

        [TestMethod]
        public void DeleteCall_FailureMessage_DeleteCallResponseException()
        {
            // Arrange
            var callId = new Guid("d64baf26-b116-4478-97b5-899de580461f");
            var failureMessage = "NORMAL TEMPORARY_FAILURE";
            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse { StatusCode = HttpStatusCode.Accepted, Content = "{\"failure\" : \"" + failureMessage + "\"}" });

            // Act + Assert
            var exception = Assert.ThrowsException<DeleteCallResponseException>(() =>
                _callsApi.DeleteCall(callId));
            Assert.AreEqual(failureMessage, exception.Message);
        }

        [TestMethod]
        public void GetCalls_ListOfCalls_ReturnsCalls()
        {
            // Arrange
            var calls = BuildCallsList();
            MockIRestClient.Setup(rc => rc.Execute<List<Call>>(It.IsAny<RestRequest>())).Returns(  
                new RestResponse<List<Call>> { StatusCode = HttpStatusCode.Accepted, Content =  JsonConvert.SerializeObject(calls) });

            // Act
            var result = _callsApi.GetCalls();

            // Assert
            calls.Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public void GetCallByGuid_OneCall_ReturnsProperCall()
        {
            // Arrange
            var calls = BuildCallsList();
            var guid = calls.First().Uuid;
            MockIRestClient.Setup(rc => rc.Execute<Call>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<Call> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(calls) });

            // Act
            var result = _callsApi.GetCall(guid);

            // Assert
            Assert.IsNotNull(result);
            calls.First().Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public void GetCallByGuid_StatusCodeNotFound_ReturnsNull()
        {
            // Arrange
            var guid = Guid.NewGuid();
            MockIRestClient.Setup(rc => rc.Execute<Call>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<Call> { StatusCode = HttpStatusCode.NotFound, Content = string.Empty });

            // Act
            var result = _callsApi.GetCall(guid);

            // Assert
            Assert.IsNull(result);
            MockIRestClient.Verify(x => x.Execute<Call>(It.IsAny<RestRequest>()), Times.Once);
        }

        private static List<Call> BuildCallsList()
        {
            var list = new List<Call>
            {
                new CallBuilder().CreateBuilder()
                    .WithUuid(new Guid("cd79587d-c71e-4bb0-9fdc-244bf9a95538"))
                    .WithCreated(DateTimeOffset.Parse("2019-10-09T12:01:22"))
                    .WithCallerIdName("Outbound Call")
                    .WithCallerIdNumber("123456789")
                    .WithDestination("123456789")
                    .WithCallState(CallState.ACTIVE)
                    .WithCallUuid("cd79587d-c71e-4bb0-9fdc-244bf9a95538")
                    .Build(),
                new CallBuilder().CreateBuilder()
                    .WithUuid(new Guid("cd79587d-c71e-4bb0-9fdc-244bf9a95538"))
                    .WithCreated(DateTimeOffset.Parse("2019-10-09T12:01:22"))
                    .WithCallerIdName("Outbound Call")
                    .WithCallerIdNumber("987654321")
                    .WithDestination("987654321")
                    .WithCallState(CallState.EARLY)
                    .WithCallUuid("fa67a5f3-bac4-48bb-ade7-efa19cd99938")
                    .Build()
            };

            return list;
        }
        }
}
