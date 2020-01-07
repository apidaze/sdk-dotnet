using APIdaze.SDK.Base;
using RestSharp;

namespace APIdaze.SDK.CredentialsValidator
{
    internal class CredentialsValidatorApi : ICredentialsValidator
    {
        private readonly Credentials credentials;
        private readonly string url;

        public CredentialsValidatorApi(Credentials credentials, string url)
        {
            this.credentials = credentials;
            this.url = url;
        }

        public bool ValidateCredentials()
        {
            return new CredentialsValidator(new RestClient(url), credentials).ValidateCredentials();
        }
    }
}