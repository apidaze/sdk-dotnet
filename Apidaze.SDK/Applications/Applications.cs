using Apidaze.SDK.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apidaze.SDK.Applications
{
    internal class Applications : BaseApiClient, IApplications
    {
        private Credentials credentials;
        private string url;

        public Applications(Credentials credentials, string url)
        {
            this.credentials = credentials;
            this.url = url;
        }

        public static Applications Create(Credentials credentials)
        {
            return Create(credentials, _url);
        }

        private static Applications Create(Credentials credentials, string url)
        {
            //add sanity check
            return new Applications(credentials, url);
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
