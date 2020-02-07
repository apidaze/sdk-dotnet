using Apidaze.SDK.Base;

namespace Apidaze.SDK
{
    public class ApplicationManager
    {
        public static IApiActionFactory CreateApiFactory(Credentials credentials,
            string url = "https://api.Apidaze.io/")
        {
            return new ApiActionFactory(credentials, url);
        }
    }
}