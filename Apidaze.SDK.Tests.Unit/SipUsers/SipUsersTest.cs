using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Apidaze.SDK.SipUsers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using static Apidaze.SDK.Tests.Unit.TestUtil;

namespace Apidaze.SDK.Tests.Unit.SipUsers
{
    /// <summary>
    ///     Defines test class SipUsertsTest.
    ///     Implements the <see cref="Apidaze.SDK.Tests.Unit.BaseTest" />
    /// </summary>
    /// <seealso cref="Apidaze.SDK.Tests.Unit.BaseTest" />
    [TestClass]
    public class SipUsertsTest : BaseTest
    {
        /// <summary>
        ///     The sip user API
        /// </summary>
        private SDK.SipUsers.SipUsers _sipUserAPI;

        /// <summary>
        ///     Startups this instance.
        /// </summary>
        [TestInitialize]
        public void Startup()
        {
            _sipUserAPI = new SDK.SipUsers.SipUsers(MockIRestClient.Object, CredentialsForTest);
        }

        /// <summary>
        ///     Defines the test method GetSipUsers_ListOfSipUsers_ReturnsSipUsersList.
        /// </summary>
        [TestMethod]
        public void GetSipUsers_ListOfSipUsers_ReturnsSipUsersList()
        {
            // Arrange
            var sipUsers = BuildSipUsersLists();
            MockIRestClient.Setup(rc => rc.Execute<List<SipUser>>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<List<SipUser>>
                    {StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(sipUsers)});

            // Act
            var result = _sipUserAPI.GetSipUsers();

            // Assert
            sipUsers.Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<List<SipUser>>(It.IsAny<RestRequest>()), Times.Once);
        }

        /// <summary>
        ///     Defines the test method CreateSipUser_SipUserWithName_ReturnsSipUser.
        /// </summary>
        [TestMethod]
        public void CreateSipUser_SipUserWithName_ReturnsSipUser()
        {
            // Arrange
            var sipUser = BuildSipUsersLists().First();
            MockIRestClient.Setup(rc => rc.Execute<SipUser>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<SipUser>
                    {StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(sipUser)});

            // Act
            var result = _sipUserAPI.CreateSipUser(sipUser.Name);

            // Assert
            Assert.AreEqual(result.Name, sipUser.Name);
        }

        /// <summary>
        ///     Defines the test method DeleteSipUser_IdUser_ReturnsNoContent.
        /// </summary>
        [TestMethod]
        public void DeleteSipUser_IdUser_ReturnsNoContent()
        {
            // Arrange
            var sipUser = BuildSipUsersLists().First();
            MockIRestClient.Setup(rc => rc.Execute(It.IsAny<RestRequest>())).Returns(
                new RestResponse<SipUser> {StatusCode = HttpStatusCode.NoContent});

            // Act
            _sipUserAPI.DeleteSipUser(sipUser.Id);

            // Assert
            MockIRestClient.Verify(x => x.Execute(It.IsAny<RestRequest>()), Times.Once);
        }

        /// <summary>
        ///     Defines the test method GetSingleSipUser_IdSipUser_ReturnsSingleSipUser.
        /// </summary>
        [TestMethod]
        public void GetSingleSipUser_IdSipUser_ReturnsSingleSipUser()
        {
            // Arrange
            var sipUser = BuildSipUsersLists().First();
            MockIRestClient.Setup(rc => rc.Execute<SipUser>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<SipUser>
                    {StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(sipUser)});

            // Act
            var result = _sipUserAPI.GetSingleSipUser(sipUser.Id);

            // Assert
            sipUser.Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<SipUser>(It.IsAny<RestRequest>()), Times.Once);
        }

        /// <summary>
        ///     Defines the test method UpdateSipUser_SipUserWithName_ReturnsUpdatedSipUser.
        /// </summary>
        [TestMethod]
        public void UpdateSipUser_SipUserWithName_ReturnsUpdatedSipUser()
        {
            // Arrange
            var sipUser = BuildSipUsersLists().First();
            MockIRestClient.Setup(rc => rc.Execute<SipUser>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<SipUser>
                    {StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(sipUser)});

            // Act
            var result = _sipUserAPI.UpdateSipUser(sipUser.Id, sipUser.Name);

            // Assert
            Assert.AreEqual(result.Name, sipUser.Name);
        }

        /// <summary>
        ///     Defines the test method ShowSipUserStatus_IdSipUser_ReturnsSipUserStatus.
        /// </summary>
        [TestMethod]
        public void ShowSipUserStatus_IdSipUser_ReturnsSipUserStatus()
        {
            // Arrange
            var sipUser = BuildSipUsersLists().First();
            var sipUserStatus = new SipUserStatus {Status = "testStatus", Uri = "testUri"};
            MockIRestClient.Setup(rc => rc.Execute<SipUserStatus>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<SipUserStatus>
                    {StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(sipUserStatus)});

            // Act
            var result = _sipUserAPI.ShowSipUserStatus(sipUser.Id);

            // Assert
            sipUserStatus.Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<SipUserStatus>(It.IsAny<RestRequest>()), Times.Once);
        }

        /// <summary>
        ///     Defines the test method ResetUserPassword_SipUserWithId_ReturnsSipUser.
        /// </summary>
        [TestMethod]
        public void ResetUserPassword_SipUserWithId_ReturnsSipUser()
        {
            // Arrange
            var sipUser = BuildSipUsersLists().First();
            MockIRestClient.Setup(rc => rc.Execute<SipUser>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<SipUser>
                    {StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(sipUser)});

            // Act
            var result = _sipUserAPI.ResetUserPassword(sipUser.Id);

            // Assert
            Assert.AreEqual(result.Sip.Password, sipUser.Sip.Password);
        }

        /// <summary>
        ///     Builds the external scripts lists.
        /// </summary>
        /// <returns>List&lt;ExternalScript&gt;.</returns>
        private List<SipUser> BuildSipUsersLists()
        {
            return new List<SipUser>
            {
                new SipUser
                {
                    Id = "1",
                    Name = "testUser1",
                    Sip = new Sip {Password = "testPassword"},
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new SipUser
                {
                    Id = "2",
                    Name = "testUser2",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };
        }
    }
}