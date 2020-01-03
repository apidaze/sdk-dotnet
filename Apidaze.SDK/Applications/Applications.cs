using Apidaze.SDK.Base;
using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace Apidaze.SDK.Applications
{
    internal class Applications : BaseApiClient, IApplications
    {
        private Credentials credentials;

        public Applications(IRestClient client, Credentials credentials) : base(client, credentials)
        {
            this.credentials = credentials;
        }
       
        private static Applications Create(IRestClient client, Credentials credentials)
        {
            //add sanity check
            return new Applications(client, credentials);
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
    }
}
