using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APIdaze.SDK.Applications;
using APIdaze.SDK.Base;
using RestSharp;

namespace APIdaze.SDK
{
    internal class ApplicationClient :  IApplications
    {
        private readonly Credentials _credentials;
        private readonly string _url;
 
        internal ApplicationClient(Credentials credentials, string url)
        {
            _credentials = credentials;
            _url = url;
        }

        public List<Application> GetApplications()
        {
            return new Applications.Applications(new RestClient(_url), _credentials).GetApplications();
        }

        public List<Application> GetApplicationsById(long id)
        {
            return new Applications.Applications(new RestClient(_url), _credentials).GetApplicationsById(id);
        }

        public List<Application> GetApplicationsByApiKey(string apiKey)
        {
            return new Applications.Applications(new RestClient(_url), _credentials).GetApplicationsByApiKey(apiKey);
        }

        public List<Application> GetApplicationsByName(string name)
        {
            return new Applications.Applications(new RestClient(_url), _credentials).GetApplicationsByName(name);
        }
    }
}
