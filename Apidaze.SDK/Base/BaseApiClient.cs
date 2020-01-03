using Apidaze.SDK.Http;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Apidaze.SDK.Base
{
    public abstract class BaseApiClient : IBaseApiClient
    {
        protected IRestClient Client;
        protected static readonly string _url = "https://api.apidaze.io/";
        private Credentials _credentials;
        private string baseUrl;

        protected BaseApiClient(IRestClient client, Credentials credentials)
        {
            Client = client;
            _credentials = credentials;
            client.BaseUrl = new Uri(_url);
        }

        protected BaseApiClient(IRestClient client, Credentials credentials, string baseUrl) : this(client, credentials)
        {
            this.baseUrl = baseUrl;
        }

        public TResponse Create<TRequest, TResponse> (TRequest request)
            where TRequest : class
            where TResponse : class, new()
        {
            var body = JsonConvert.SerializeObject(request);
            RestRequest restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);

            IRestResponse response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);

            var deserializedResponse = JsonConvert.DeserializeObject<TResponse>(response.Content);
            return deserializedResponse;
        }

        public TResponse FindAll<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class, new()
        {
            var restRequest = new RestRequest(Method.GET);

            IRestResponse response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);

            var deserializedResponse = JsonConvert.DeserializeObject<TResponse>(response.Content);
            return deserializedResponse;
        }

        public TResponse FindById<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class, new()
        {
            throw new NotImplementedException();
        }

        public TResponse Update<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class, new()
        {
            throw new NotImplementedException();
        }

        public TResponse Delete<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class, new()
        {
            throw new NotImplementedException();
        }

        internal void EnsureSuccessResponse(IRestResponse response)
        {
            if (new HttpStatusCode[] { HttpStatusCode.InternalServerError, HttpStatusCode.BadRequest }
                .Contains(response.StatusCode))
            {
                var newException = new InvalidOperationException(response.StatusDescription);
                newException.Data["DetailHtml"] = response.Content;
                throw newException;
            }
        }
    }
}
