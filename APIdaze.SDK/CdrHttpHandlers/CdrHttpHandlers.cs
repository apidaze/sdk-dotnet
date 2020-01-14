using System;
using System.Collections.Generic;
using System.Text;
using APIdaze.SDK.Base;
using APIdaze.SDK.Calls;
using Newtonsoft.Json;
using RestSharp;

namespace APIdaze.SDK.CdrHttpHandlers
{
    public class CdrHttpHandlers : BaseApiClient, ICdrHttpHandlers
    {
        protected override string Resource => "/cdrhttphandlers";

        public CdrHttpHandlers(IRestClient client, Credentials credentials) : base(client, credentials)
        {
        }

        public List<CdrHttpHandler> GetCdrHttpHandlers()
        {
            return FindAll<CdrHttpHandler>();
        }

        public CdrHttpHandler CreateCdrHttpHandler(string name, Uri url)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("origin must not be null or empty");
            if (url == null) throw new ArgumentException("destination must not be null or empty");

            var requestBody = new Dictionary<string, string> { { "name", name }, { "url", url.ToString() } };
            return Create<CdrHttpHandler>(requestBody);
        }

        public CdrHttpHandler UpdateCdrHttpHandler(long id, string name, Uri url)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("origin must not be null or empty");
            if (url == null) throw new ArgumentException("destination must not be null or empty");

            var requestBody = new Dictionary<string, string> { { "name", name }, { "url", url.ToString() } };
            return Update<CdrHttpHandler>(id.ToString(), requestBody);
        }

        public CdrHttpHandler UpdateCdrHttpHandlerName(long id, string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("origin must not be null or empty");

            var requestBody = new Dictionary<string, string> { { "name", name } };
            return Update<CdrHttpHandler>(id.ToString(), requestBody);
        }

        public CdrHttpHandler UpdateCdrHttpHandlerUrl(long id, Uri url)
        {
            if (url == null) throw new ArgumentException("destination must not be null or empty");

            var requestBody = new Dictionary<string, string> { { "url", url.ToString() } };
            return Update<CdrHttpHandler>(id.ToString(), requestBody);
        }

        public void DeleteCdrHttpHandler(long id)
        {
            Delete<CdrHttpHandler>(id.ToString());
        }
    }
}
