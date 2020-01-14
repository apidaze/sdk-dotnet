using APIdaze.SDK.Base;

namespace APIdaze.SDK
{
    public class ApplicationManager
    {
        public static IApiActionFactory CreateApiFactory(Credentials credentials,
            string url = "https://api.apidaze.io/")
        {
            return new ApiActionFactory(credentials, url);
        }
    }
}