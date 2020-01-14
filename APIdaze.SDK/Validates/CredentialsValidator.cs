using APIdaze.SDK.Base;
using RestSharp;
using System.Net;

namespace APIdaze.SDK.Validates
{
    public class CredentialsValidator : BaseApiClient, ICredentialsValidator
    {
        protected override string Resource => "/validates";

        public CredentialsValidator(IRestClient client, Credentials credentials) : base(client, credentials)
        {
        }

        public bool ValidateCredentials()
        {
            var restRequest = AuthenticateRequest();
            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);
            return response.StatusCode != HttpStatusCode.Unauthorized;
        }
    }
}