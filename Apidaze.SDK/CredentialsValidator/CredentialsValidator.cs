using System.Net;
using APIdaze.SDK.Base;
using RestSharp;

namespace APIdaze.SDK.CredentialsValidator
{
    public class CredentialsValidator : BaseApiClient, ICredentialsValidator
    {
        private readonly Credentials credentials;

        public CredentialsValidator(IRestClient client, Credentials credentials) : base(client, credentials)
        {
            this.credentials = credentials;
        }

        public bool ValidateCredentials()
        {
            var restRequest = new RestRequest("{api_key}/validates", Method.GET);
            restRequest.AddUrlSegment("api_key", credentials.ApiKey);

            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddParameter("api_secret", credentials.ApiSecret);

            var response = Client.Execute(restRequest);
            return response.StatusCode != HttpStatusCode.Unauthorized;
        }
    }
}