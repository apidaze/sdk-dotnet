using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace APIdaze.SDK.Base
{
    public abstract class BaseApiClient : IBaseApiClient
    {
        private readonly Credentials _credentials;
        protected readonly IRestClient Client;
        protected abstract string Resource { get; }

        protected BaseApiClient(IRestClient client, Credentials credentials)
        {
            Client = client;
            _credentials = credentials;
        }

        protected RestRequest AuthenticateRequest()
        {
            var restRequest = new RestRequest("{api_key}" + Resource);
            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddUrlSegment("api_key", _credentials.ApiKey);
            restRequest.AddQueryParameter("api_secret", _credentials.ApiSecret);

            return restRequest;
        }

        public T Create<T>(Dictionary<string, string> requestParams) where T : new()
        {
            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.POST;

            foreach (var nameValueParameter in requestParams)
            {
                restRequest.AddParameter(nameValueParameter.Key, nameValueParameter.Value);
            }

            var response = Client.Execute<T>(restRequest);
            EnsureSuccessResponse(response);

            var deserializedResponse = JsonConvert.DeserializeObject<T>(response.Content);
            return deserializedResponse;
        }

        public List<T> FindAll<T>()
            where T : new()
        {
            var restRequest = AuthenticateRequest();
            var response = Client.Execute<List<T>>(restRequest);
            EnsureSuccessResponse(response);

            var deserializedResponse = JsonConvert.DeserializeObject<List<T>>(response.Content);
            return deserializedResponse;
        }

        public List<T> FindByParameter<T>(string name, string value) where T : new()
        {
            throw new NotImplementedException();
        }

        public T FindById<T>(string id)
            where T : new()
        {
            var restRequest = AuthenticateRequest();
            restRequest.AddParameter("uuid", id);
            var response = Client.Execute<T>(restRequest);

            EnsureSuccessResponse(response);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return default;
            }
            var deserializedResponse = JsonConvert.DeserializeObject<IEnumerable<T>>(response.Content);
            return deserializedResponse.FirstOrDefault();
        }

        public T Update<T>(string id, Dictionary<string, string> requestParams) where T : new()
        {
            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.PUT;

            foreach (var nameValueParameter in requestParams)
            {
                restRequest.AddParameter(nameValueParameter.Key, nameValueParameter.Value);
            }
            restRequest.AddParameter("uuid", id);

            var response = Client.Execute<T>(restRequest);
            EnsureSuccessResponse(response);

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public void Delete<T>(string id) where T : new()
        {
            var restRequest = AuthenticateRequest();
            restRequest.Resource = Resource + "/{uuid}";
            restRequest.Method = Method.DELETE;
            restRequest.AddUrlSegment("uuid", id);

            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);
        }

        internal static void EnsureSuccessResponse(IRestResponse response)
        {
            if (!new[] { HttpStatusCode.InternalServerError, HttpStatusCode.BadRequest }
                .Contains(response.StatusCode)) return;
            var newException = new InvalidOperationException(response.StatusDescription);
            throw newException;
        }
    }
}