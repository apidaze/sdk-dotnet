﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APIdaze.SDK.Base;
using RestSharp;

namespace APIdaze.SDK.ExternalScripts
{
    public class ExternalScripts : BaseApiClient, IExternalScripts
    {
        protected override string Resource => "/externalscripts";

        private readonly int MaxNameLength = 40;
        public ExternalScripts(IRestClient client, Credentials credentials) : base(client, credentials)
        {
        }

        public List<ExternalScript> GetExternalScripts()
        {
            return FindAll<ExternalScript>().ToList();
        }

        public ExternalScript GetExternalScript(long id)
        {
            return FindById<ExternalScript>(id.ToString());
        }

        public ExternalScript UpdateExternalScript(long id, string name, Uri url)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("origin must not be null or empty");
            if (url == null) throw new ArgumentException("destination must not be null or empty");
            if (name.Length > MaxNameLength)
            {
                throw new ArgumentException("name: maximum " + MaxNameLength + " characters long");
            }

            var requestBody = new Dictionary<string, string> { { "name", name }, { "url", url.ToString() } };
            return Update<ExternalScript>(id.ToString(), requestBody);
        }

        public ExternalScript UpdateExternalScriptUrl(long id, Uri url)
        {
            if (url == null) throw new ArgumentException("destination must not be null or empty");

            var requestBody = new Dictionary<string, string> { { "url", url.ToString() } };

            return Update<ExternalScript>(id.ToString(), requestBody);
        }
    }
}
