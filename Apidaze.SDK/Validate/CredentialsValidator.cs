using System.Net;
using Apidaze.SDK.Base;
using RestSharp;

namespace Apidaze.SDK.Validate
{
    public class CredentialsValidator : BaseApiClient, ICredentialsValidator
    {
        public CredentialsValidator(IRestClient client, Credentials credentials) : base(client, credentials)
        {
        }

        protected override string Resource => "/validates";

        public bool ValidateCredentials()
        {
            var restRequest = AuthenticateRequest();
            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);
            return response.StatusCode != HttpStatusCode.Unauthorized;
        }
    }
}