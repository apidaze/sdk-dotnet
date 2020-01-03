using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Apidaze.SDK.Base;
using Apidaze.SDK.CredentialsValidator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Apidaze.SDK.CredentialsValidator
{
    public class CredentialsValidator : BaseApiClient, ICredentialsValidator
    {
        private readonly string baseUrl;
        private readonly Credentials credentials;

        public CredentialsValidator(IRestClient client, Credentials credentials, string baseUrl) : base(client, credentials, baseUrl)
        {
            this.credentials = credentials;
            this.baseUrl = baseUrl;
        }

        public static CredentialsValidator Create(IRestClient client, Credentials credentials)
        {
            return Create(client, credentials, _url);
        }

        public static CredentialsValidator Create(IRestClient client, Credentials credentials, string baseUrl)
        {
            return new CredentialsValidator(client, credentials, baseUrl);
        }

        public bool ValidateCredentials()
        {
            RestRequest restRequest = new RestRequest("/validates", Method.GET);
            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddParameter("api_secret", credentials.ApiSecret);
            restRequest.AddUrlSegment("api_key", credentials.ApiKey);

            IRestResponse response = Client.Execute(restRequest);
            return response.StatusCode != HttpStatusCode.Unauthorized;
        }
    }
}
