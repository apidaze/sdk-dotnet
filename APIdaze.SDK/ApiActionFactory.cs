using APIdaze.SDK.Applications;
using APIdaze.SDK.Base;
using APIdaze.SDK.CredentialsValidator;
using APIdaze.SDK.Messages;

namespace APIdaze.SDK
{
    internal class ApiActionFactory : IApiActionFactory
    {
        private readonly Credentials _credentials;
        private readonly string _url;

        internal ApiActionFactory(Credentials credentials, string url = "https://api.apidaze.io/")
        {
            _credentials = credentials;
            _url = url;
        }

        public IMessageAction CreateMessage()
        {
            return new MessageAction(_credentials, _url);
        }

        public ICredentialsValidator CreateCredentialsValidatorApi()
        {
            return new CredentialsValidatorApi(_credentials, _url);
        }

        public IApplications CreateApplicationsApi()
        {
            return new ApplicationClient(_credentials, _url);
        }
    }
}