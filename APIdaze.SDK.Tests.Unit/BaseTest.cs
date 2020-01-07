using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace APIdaze.SDK.Tests.Unit
{
    [TestClass]
    public class BaseTest
    {
        protected Mock<IRestClient> mockIRestClient;
        
        [TestInitialize]
        public void BaseTestInit()
        {
            mockIRestClient = new Mock<IRestClient>();
        }
    }
}