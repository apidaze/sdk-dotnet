using Apidaze.SDK.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apidaze.SDK.Messages
{
    public class Message : BaseApiClient, IMessage
    {
        private Credentials credentials;
        private string url;

        internal static readonly string basePath = "sms";

        public Message(Credentials credentials, string url)
        {
            this.credentials = credentials;
            this.url = url;
        }

        public static Message Create(Credentials credentials)
        {
            return Create(credentials, _url);
        }

        public string SendTextMessage(PhoneNumber from, PhoneNumber to, string request)
        {
            if (string.IsNullOrEmpty(request)) throw new ArgumentException("body must not be null or empty");

            var body = JsonConvert.SerializeObject(new { from, to, request });
            

            RestRequest restRequest = new RestRequest(Method.PUT);
            restRequest.AddParameter("text/json", body, ParameterType.RequestBody);

            IRestResponse response = _client.Execute(restRequest);
            EnsureSuccessResponse(response);

            var deserializedResponse = JsonConvert.DeserializeObject<JObject>(response.Content).ToString(Formatting.None);
            return deserializedResponse;

        }

        private static Message Create(Credentials credentials, string url)
        {
            //add sanity check
            return new Message(credentials, url);
        }
    }
}
