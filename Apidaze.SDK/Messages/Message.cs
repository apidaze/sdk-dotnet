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
        private Base.Credentials credentials;
        private string url;

        internal static readonly string basePath = "sms/send";

        public Message(IRestClient client, Credentials credentials, string url) : base(client, credentials)
        {
            this.credentials = credentials;
            this.url = url;
        }

        public static Message Create(IRestClient client, Credentials credentials)
        {
            //add sanity check
            return new Message(client, credentials, _url);
        }

        public string SendTextMessage(PhoneNumber from, PhoneNumber to, string bodyMessage)
        {
            if (string.IsNullOrEmpty(bodyMessage)) throw new ArgumentException("body must not be null or empty");

            RestRequest restRequest = new RestRequest(basePath, Method.POST) {RequestFormat = DataFormat.Json};
            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddParameter("api_secret", credentials.ApiSecret);
            restRequest.AddUrlSegment("api_key", credentials.ApiKey);
            restRequest.AddJsonBody(new {from = from.ToString(), to = to.ToString(), body = bodyMessage});

            IRestResponse response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);

            var deserializedResponse = JsonConvert.DeserializeObject<JObject>(response.Content).ToString(Formatting.None);
            return deserializedResponse;

        }

      
    }
}
