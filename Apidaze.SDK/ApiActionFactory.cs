﻿using Apidaze.SDK.Applications;
using Apidaze.SDK.Base;
using Apidaze.SDK.Calls;
using Apidaze.SDK.CdrHttpHandlers;
using Apidaze.SDK.ExternalScripts;
using Apidaze.SDK.Validate;
using Apidaze.SDK.Messages;
using Apidaze.SDK.Recordings;
using RestSharp;

namespace Apidaze.SDK
{
   
    internal class ApiActionFactory : IApiActionFactory
    {
        /// <summary>
        /// The credentials
        /// </summary>
        private readonly Credentials _credentials;

        /// <summary>
        /// The URL
        /// </summary>
        private readonly string _url;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiActionFactory" /> class.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <param name="url">The URL.</param>
        internal ApiActionFactory(Credentials credentials, string url = "https://api4.apidaze.io/")
        {
            _credentials = credentials;
            _url = url;
        }

        /// <summary>
        /// Creates the message API.
        /// </summary>
        /// <returns>IMessage.</returns>
        public IMessage CreateMessageApi()
        {
            return new Message(new RestClient(_url), _credentials);
        }

        /// <summary>
        /// Creates the credentials validator API.
        /// </summary>
        /// <returns>ICredentialsValidator.</returns>
        public ICredentialsValidator CreateCredentialsValidatorApi()
        {
            return new CredentialsValidator(new RestClient(_url), _credentials);
        }

        /// <summary>
        /// Creates the calls API.
        /// </summary>
        /// <returns>ICalls.</returns>
        public ICalls CreateCallsApi()
        {
            return new Calls.Calls(new RestClient(_url), _credentials);
        }

        /// <summary>
        /// Creates the applications API.
        /// </summary>
        /// <returns>IApplications.</returns>
        public IApplications CreateApplicationsApi()
        {
            return new Applications.Applications(new RestClient(_url), _credentials);
        }

        /// <summary>
        /// Creates the CDR HTTP handlers API.
        /// </summary>
        /// <returns>ICdrHttpHandlers.</returns>
        public ICdrHttpHandlers CreateCdrHttpHandlersApi()
        {
            return new CdrHttpHandlers.CdrHttpHandlers(new RestClient(_url), _credentials);
        }

        /// <summary>
        /// Creates the recordings API.
        /// </summary>
        /// <returns>IRecordings.</returns>
        public IRecordings CreateRecordingsApi()
        {
            return new Recordings.Recordings(new RestClient(_url), _credentials);
        }

        /// <summary>
        /// Creates the external scripts API.
        /// </summary>
        /// <returns>IExternalScripts.</returns>
        public IExternalScripts CreateExternalScriptsApi()
        {
            return new ExternalScripts.ExternalScripts(new RestClient(_url), _credentials);
        }
    }
}