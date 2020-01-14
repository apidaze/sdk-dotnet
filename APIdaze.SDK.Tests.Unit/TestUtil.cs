using APIdaze.SDK.Base;

namespace APIdaze.SDK.Tests.Unit
{
    public class TestUtil
    {
        private static readonly string API_KEY = "some-api-key";
        private static readonly string API_SECRET = "some-api-secret";
        public static readonly Credentials Credentials = new Credentials(API_KEY, API_SECRET);
    }
}