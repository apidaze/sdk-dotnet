using Apidaze.SDK.Base;

namespace Apidaze.SDK
{
    /// <summary>
    /// Class ApplicationManager.
    /// </summary>
    public class ApplicationManager
    {
        /// <summary>
        /// Creates the API factory.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="url">The URL.</param>
        /// <returns>IApiActionFactory.</returns>
        public static IApiActionFactory CreateApiFactory(Credentials credentials, string url = "https://api.apidaze.io/")
        {
            return new ApiActionFactory(credentials, url);
        }
    }
}