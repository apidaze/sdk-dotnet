using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace Apidaze.SDK.Tests.Unit
{
    /// <summary>
    /// Defines test class BaseTest.
    /// </summary>
    [TestClass]
    public class BaseTest
    {
        /// <summary>
        /// The mock i rest client
        /// </summary>
        protected Mock<IRestClient> MockIRestClient;

        /// <summary>
        /// Bases the test initialize.
        /// </summary>
        [TestInitialize]
        public void BaseTestInit()
        {
            MockIRestClient = new Mock<IRestClient>();
        }
    }
}