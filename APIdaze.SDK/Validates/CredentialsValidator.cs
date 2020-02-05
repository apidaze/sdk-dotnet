﻿using System.Net;
using APIdaze.SDK.Base;
using RestSharp;

namespace APIdaze.SDK.Validates
{
    /// <summary>
    /// Class CredentialsValidator.
    /// Implements the <see cref="APIdaze.SDK.Base.BaseApiClient" />
    /// Implements the <see cref="APIdaze.SDK.Validates.ICredentialsValidator" />
    /// </summary>
    /// <seealso cref="APIdaze.SDK.Base.BaseApiClient" />
    /// <seealso cref="APIdaze.SDK.Validates.ICredentialsValidator" />
    public class CredentialsValidator : BaseApiClient, ICredentialsValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialsValidator" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="credentials">The credentials.</param>
        public CredentialsValidator(IRestClient client, Credentials credentials) : base(client, credentials)
        {
        }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <value>The resource.</value>
        protected override string Resource => "/validates";

        /// <summary>
        /// Validates the credentials.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ValidateCredentials()
        {
            var restRequest = AuthenticateRequest();
            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);
            return response.StatusCode != HttpStatusCode.Unauthorized;
        }
    }
}