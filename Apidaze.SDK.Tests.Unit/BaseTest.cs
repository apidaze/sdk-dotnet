using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace Apidaze.SDK.Tests.Unit
{
    // MethodName_StateUnderTest_ExpectedBehavior
    [TestClass]
    public class BaseTest
    {
        protected Mock<IRestClient> MockIRestClient;

        [TestInitialize]
        public void BaseTestInit()
        {
            MockIRestClient = new Mock<IRestClient>();
        }
    }
}