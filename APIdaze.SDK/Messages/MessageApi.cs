using System;
using APIdaze.SDK.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace APIdaze.SDK.Messages
{
    public class MessageApi : BaseApiClient, IMessageAction
    {
        internal static readonly string basePath = "/sms/send";
        private readonly Credentials credentials;

        public MessageApi(IRestClient client, Credentials credentials) : base(client, credentials)
        {
            this.credentials = credentials;
        }

        public string SendTextMessage(PhoneNumber from, PhoneNumber to, string bodyMessage)
        {
            if (string.IsNullOrEmpty(bodyMessage)) throw new ArgumentException("body must not be null or empty");

            var restRequest = new RestRequest("{api_key}"+basePath, Method.POST) {RequestFormat = DataFormat.Json};
            restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            restRequest.AddUrlSegment("api_key", credentials.ApiKey);
            restRequest.AddParameter("api_secret", credentials.ApiSecret);

            restRequest.AddJsonBody(new {from = from.ToString(), to = to.ToString(), body = bodyMessage});

            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);

            var deserializedResponse =
                JsonConvert.DeserializeObject<dynamic>(response.Content).ToString(Formatting.None);
            return deserializedResponse as string;
        }
    }
}