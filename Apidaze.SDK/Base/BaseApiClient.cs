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
        internal readonly RestClient _client;
        internal readonly static string _url = "https://api.apidaze.io";

        public BaseApiClient()
        {
            _client = new RestClient(_url);
        }

        protected void AuthenticateUrl ()
        {
            //TODO
            _client.Authenticator = OAuth1Authenticator.ForRequestToken("", "" );
        }

        public TResponse Create<TRequest, TResponse> (TRequest request)
            where TRequest : class
            where TResponse : class, new()
        {
            var body = JsonConvert.SerializeObject(request);

            RestRequest restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            restRequest.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);

            IRestResponse response = _client.Execute(restRequest);
            EnsureSuccessResponse(response);

            var deserializedResponse = JsonConvert.DeserializeObject<TResponse>(response.Content);
            return deserializedResponse;
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
