using APIdaze.SDK.Applications;
using APIdaze.SDK.Base;
using APIdaze.SDK.Calls;
using APIdaze.SDK.CdrHttpHandlers;
using APIdaze.SDK.Validates;
using APIdaze.SDK.Messages;
using RestSharp;

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

        public IMessage CreateMessageApi()
        {
            return new Message(new RestClient(_url), _credentials);
        }

        public ICredentialsValidator CreateCredentialsValidatorApi()
        {
            return new CredentialsValidator(new RestClient(_url), _credentials);
        }

        public ICalls CreateCallsApi()
        {
            return new Calls.Calls(new RestClient(_url), _credentials);
        }

        public IApplications CreateApplicationsApi()
        {
            return new Applications.Applications(new RestClient(_url), _credentials);
        }

        public ICdrHttpHandlers CreateCdrHttpHandlersApi()
        {
            return new CdrHttpHandlers.CdrHttpHandlers(new RestClient(_url), _credentials);
        }
    }
}