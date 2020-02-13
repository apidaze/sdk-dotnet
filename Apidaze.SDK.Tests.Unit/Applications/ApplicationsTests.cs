using Apidaze.SDK.Applications;
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
using ApplicationClient = Apidaze.SDK.Applications.Applications;

namespace Apidaze.SDK.Tests.Unit.Applications
{
    /// <summary>
    /// Defines test class ApplicationsTests.
    /// Implements the <see cref="APIdaze.SDK.Tests.Unit.BaseTest" />
    /// </summary>
    /// <seealso cref="APIdaze.SDK.Tests.Unit.BaseTest" />
    [TestClass]
    public class ApplicationsTests : BaseTest
    {
        /// <summary>
        /// The application client API
        /// </summary>
        private ApplicationClient _applicationClientApi;

        /// <summary>
        /// Startups this instance.
        /// </summary>
        [TestInitialize]
        public void Startup()
        {
            _applicationClientApi = new ApplicationClient(MockIRestClient.Object, CredentialsForTest);
        }

        /// <summary>
        /// Defines the test method GetApplications_ListOfApplications_ReturnsApplicationsList.
        /// </summary>
        [TestMethod]
        public void GetApplications_ListOfApplications_ReturnsApplicationsList()
        {
            // Arrange
            var applications = BuildApplicationsList();
            MockIRestClient.Setup(rc => rc.Execute<List<Application>>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<List<Application>> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(applications) });

            // Act
            var result = _applicationClientApi.GetApplications();

            // Assert
            applications.Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<List<Application>>(It.IsAny<RestRequest>()), Times.Once);
        }

        /// <summary>
        /// Defines the test method GetApplicationsById_OneApplication_ReturnsOneApplication.
        /// </summary>
        [TestMethod]
        public void GetApplicationsById_OneApplication_ReturnsOneApplication()
        {
            // Arrange
            var applications = BuildApplicationsList();
            var id = applications.First().Id;
            MockIRestClient.Setup(rc => rc.Execute<List<Application>>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<List<Application>> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(applications) });

            // Act
            var result = _applicationClientApi.GetApplicationsById(id);

            // Assert
            applications.Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<List<Application>>(It.IsAny<RestRequest>()), Times.Once);
        }

        /// <summary>
        /// Defines the test method GetApplicationsByApiKey_OneApplication_ReturnsOneApplication.
        /// </summary>
        [TestMethod]
        public void GetApplicationsByApiKey_OneApplication_ReturnsOneApplication()
        {
            // Arrange
            var applications = BuildApplicationsList();
            var apiKey = applications.First().ApiKey;
            MockIRestClient.Setup(rc => rc.Execute<List<Application>>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<List<Application>> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(applications) });

            // Act
            var result = _applicationClientApi.GetApplicationsByApiKey(apiKey);

            // Assert
            applications.Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<List<Application>>(It.IsAny<RestRequest>()), Times.Once);
        }

        /// <summary>
        /// Defines the test method GetApplicationsByApiName_OneApplication_ReturnsOneApplication.
        /// </summary>
        [TestMethod]
        public void GetApplicationsByApiName_OneApplication_ReturnsOneApplication()
        {
            // Arrange
            var applications = BuildApplicationsList();
            var name = applications.First().Name;
            MockIRestClient.Setup(rc => rc.Execute<List<Application>>(It.IsAny<RestRequest>())).Returns(
                new RestResponse<List<Application>> { StatusCode = HttpStatusCode.Accepted, Content = JsonConvert.SerializeObject(applications) });

            // Act
            var result = _applicationClientApi.GetApplicationsByName(name);

            // Assert
            applications.Should().BeEquivalentTo(result);
            MockIRestClient.Verify(x => x.Execute<List<Application>>(It.IsAny<RestRequest>()), Times.Once);
        }
        /// <summary>
        /// Builds the applications list.
        /// </summary>
        /// <returns>List&lt;Application&gt;.</returns>
        private List<Application> BuildApplicationsList()
        {
            return new List<Application>
            {
                new Application
                {
                    Id = 1L,
                    AccountId = 2L,
                    ApplicationId = "appId: 1234",
                    ApiKey = "appKey: 111",
                    ApiSecret = "test",
                    Name = "name1",
                    FsAddress = "fsAddress1",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };
        }
    }
}
