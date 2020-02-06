using Apidaze.SDK.Base;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace Apidaze.SDK.Messages
{
    public class Message : BaseApiClient, IMessage
    {
        protected override string Resource => "/sms/send";

        public Message(IRestClient client, Credentials credentials) : base(client, credentials)
        {
        }

        public string SendTextMessage(PhoneNumber from, PhoneNumber to, string bodyMessage)
        {
            if (string.IsNullOrEmpty(bodyMessage)) throw new ArgumentException("body must not be null or empty");

            var restRequest = BuildRequest(from, to, bodyMessage);

            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);

            var deserializedResponse =
                JsonConvert.DeserializeObject<dynamic>(response.Content).ToString(Formatting.None);
            return deserializedResponse;
        }

        private RestRequest BuildRequest(PhoneNumber from, PhoneNumber to, string bodyMessage)
        {
            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.POST;
            restRequest.AddParameter("from", from.ToString());
            restRequest.AddParameter("to", to.ToString());
            restRequest.AddParameter("body", bodyMessage);
            return restRequest;
        }
    }
}