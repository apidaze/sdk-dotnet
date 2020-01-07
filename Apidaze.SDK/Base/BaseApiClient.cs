using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace APIdaze.SDK.Base
{
    public abstract class BaseApiClient : IBaseApiClient
    {
        protected static readonly string _url = "https://api.apidaze.io/";
        private Credentials _credentials;
        protected IRestClient Client;

        protected BaseApiClient(IRestClient client, Credentials credentials)
        {
            Client = client;
            _credentials = credentials;
        }

        public TResponse FindAll<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class, new()
        {
            var restRequest = new RestRequest(Method.GET);

            var response = Client.Execute(restRequest);
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
            if (new[] {HttpStatusCode.InternalServerError, HttpStatusCode.BadRequest}
                .Contains(response.StatusCode))
            {
                var newException = new InvalidOperationException(response.StatusDescription);
                newException.Data["DetailHtml"] = response.Content;
                throw newException;
            }
        }
    }
}