using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace Apidaze.SDK.Base
{
    /// <summary>
    ///     Class BaseApiClient.
    ///     Implements the <see cref="Apidaze.SDK.Base.IBaseApiClient" />
    /// </summary>
    /// <seealso cref="Apidaze.SDK.Base.IBaseApiClient" />
    public abstract class BaseApiClient : IBaseApiClient
    {
        /// <summary>
        ///     The credentials
        /// </summary>
        private readonly Credentials _credentials;

        /// <summary>
        ///     The client
        /// </summary>
        protected readonly IRestClient Client;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseApiClient" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="credentials">The credentials.</param>
        protected BaseApiClient(IRestClient client, Credentials credentials)
        {
            Client = client;
            _credentials = credentials;
        }

        /// <summary>
        ///     Gets the resource.
        /// </summary>
        /// <value>The resource.</value>
        protected abstract string Resource { get; }

        /// <summary>
        ///     Creates the specified request parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestParams">The request parameters.</param>
        /// <returns>T.</returns>
        public T Create<T>(Dictionary<string, string> requestParams, string resource = "") where T : new()
        {
            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.POST;

            if (!string.IsNullOrEmpty(resource))
            {
                restRequest.Resource += "/{id}" + resource;
                foreach (var idParam in requestParams) restRequest.AddUrlSegment(idParam.Key, idParam.Value);
            }
            else
            {
                foreach (var nameValueParameter in requestParams)
                    restRequest.AddParameter(nameValueParameter.Key, nameValueParameter.Value);
            }

            var response = Client.Execute<T>(restRequest);
            EnsureSuccessResponse(response);

            var deserializedResponse = JsonConvert.DeserializeObject<T>(response.Content);
            return deserializedResponse;
        }

        /// <summary>
        ///     Finds all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public IEnumerable<T> FindAll<T>()
        {
            var restRequest = AuthenticateRequest();
            var response = Client.Execute<List<T>>(restRequest);
            EnsureSuccessResponse(response);

            var deserializedResponse = JsonConvert.DeserializeObject<IEnumerable<T>>(response.Content);
            return deserializedResponse;
        }

        /// <summary>
        ///     Finds the by parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public IEnumerable<T> FindByParameter<T>(string name, string value)
            where T : new()
        {
            var restRequest = AuthenticateRequest();
            restRequest.AddQueryParameter(name, value);
            var response = Client.Execute<List<T>>(restRequest);

            EnsureSuccessResponse(response);

            var deserializedResponse = JsonConvert.DeserializeObject<IEnumerable<T>>(response.Content);
            return deserializedResponse;
        }

        /// <summary>
        ///     Finds the by identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>T.</returns>
        public T FindById<T>(string id, string resource = "")
            where T : new()
        {
            var restRequest = AuthenticateRequest();
            restRequest.Resource += "/{id}" + resource;
            restRequest.AddUrlSegment("id", id);
            var response = Client.Execute<T>(restRequest);

            EnsureSuccessResponse(response);
            if (response.StatusCode == HttpStatusCode.NotFound) return default;

            var deserializedResponse = JsonConvert.DeserializeObject<T>(response.Content);
            return deserializedResponse;
        }

        /// <summary>
        ///     Updates the specified identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="requestParams">The request parameters.</param>
        /// <returns>T.</returns>
        public T Update<T>(string id, Dictionary<string, string> requestParams) where T : new()
        {
            var restRequest = AuthenticateRequest();
            restRequest.Resource += "/{id}";
            restRequest.Method = Method.PUT;
            restRequest.AddUrlSegment("id", id);

            foreach (var nameValueParameter in requestParams)
                restRequest.AddParameter(nameValueParameter.Key, nameValueParameter.Value);

            var response = Client.Execute<T>(restRequest);
            EnsureSuccessResponse(response);

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(string id)
        {
            var restRequest = AuthenticateRequest();
            restRequest.Resource += "/{id}";
            restRequest.Method = Method.DELETE;
            restRequest.AddUrlSegment("id", id);

            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);
        }

        /// <summary>
        ///     Authenticates the request.
        /// </summary>
        /// <returns>RestRequest.</returns>
        protected RestRequest AuthenticateRequest()
        {
            var restRequest = new RestRequest("{api_key}" + Resource);
            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddUrlSegment("api_key", _credentials.ApiKey);
            restRequest.AddQueryParameter("api_secret", _credentials.ApiSecret);

            return restRequest;
        }

        /// <summary>
        ///     Ensures the success response.
        /// </summary>
        /// <param name="response">The response.</param>
        internal static void EnsureSuccessResponse(IRestResponse response)
        {
            if (!new[] {HttpStatusCode.InternalServerError, HttpStatusCode.BadRequest}
                .Contains(response.StatusCode)) return;
            var newException = new InvalidOperationException(response.Content);
            throw newException;
        }
    }
}