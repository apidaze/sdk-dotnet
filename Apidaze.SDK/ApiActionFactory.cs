using Apidaze.SDK.Applications;
using Apidaze.SDK.Base;
using Apidaze.SDK.Calls;
using Apidaze.SDK.CdrHttpHandlers;
using Apidaze.SDK.ExternalScripts;
using Apidaze.SDK.Validates;
using Apidaze.SDK.Messages;
using Apidaze.SDK.Recordings;
using RestSharp;

namespace Apidaze.SDK
{
    internal class ApiActionFactory : IApiActionFactory
    {
        private readonly Credentials _credentials;
        private readonly string _url;

        internal ApiActionFactory(Credentials credentials, string url = "https://api4.apidaze.io/")
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

        public IRecordings CreateRecordingsApi()
        {
            return new Recordings.Recordings(new RestClient(_url), _credentials);
        }

        public IExternalScripts CreateExternalScriptsApi()
        {
            return new ExternalScripts.ExternalScripts(new RestClient(_url), _credentials);
        }
    }
}