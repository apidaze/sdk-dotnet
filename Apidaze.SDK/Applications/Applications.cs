using System;
using System.Collections.Generic;
using APIdaze.SDK.Base;
using RestSharp;

namespace APIdaze.SDK.Applications
{
    internal class Applications : BaseApiClient, IApplications
    {
        private Credentials _credentials;

        public Applications(IRestClient client, Credentials credentials) : base(client, credentials)
        {
            this._credentials = credentials;
        }

        public List<Application> GetApplications()
        {
            throw new NotImplementedException();
        }

        public List<Application> GetApplicationsByApiKey(string apiKey)
        {
            throw new NotImplementedException();
        }

        public List<Application> GetApplicationsById(long id)
        {
            throw new NotImplementedException();
        }

        public List<Application> GetApplicationsByName(string name)
        {
            throw new NotImplementedException();
        }

        private static Applications Create(IRestClient client, Credentials credentials)
        {
            //add sanity check
            return new Applications(client, credentials);
        }
    }
}