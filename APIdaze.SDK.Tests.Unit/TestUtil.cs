using APIdaze.SDK.Base;

namespace APIdaze.SDK.Tests.Unit
{
    public class TestUtil
    {
        public static readonly string API_KEY = "some-api-key";
        public static readonly string API_SECRET = "some-api-secret";
        public static readonly string BASE_URL = "http://localhost";
        public static readonly Credentials CREDENTIALS = new Credentials(API_KEY, API_SECRET);
    }
}