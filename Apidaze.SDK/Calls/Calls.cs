using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apidaze.SDK.Base;
using Apidaze.SDK.Messages;
using Newtonsoft.Json;
using RestSharp;

namespace Apidaze.SDK.Calls
{
    public class Calls : BaseApiClient, ICalls
    {
        public Calls(IRestClient client, Credentials credentials) : base(client, credentials)
        {
        }

        protected override string Resource => "/calls";

        public Guid CreateCall(PhoneNumber callerId, string origin, string destination, string callType)
        {
            if (string.IsNullOrEmpty(origin)) throw new ArgumentException("origin must not be null or empty");
            if (string.IsNullOrEmpty(destination)) throw new ArgumentException("destination must not be null or empty");

            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.POST;
            restRequest.AddParameter("callerid", callerId.ToString());
            restRequest.AddParameter("origin", origin);
            restRequest.AddParameter("destination", destination);
            restRequest.AddParameter("type", callType);

            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);

            var deserializedResponse =
                JsonConvert.DeserializeObject<ResponseCall>(response.Content);
            var guidValue = deserializedResponse.Ok;
            if (guidValue != null) return new Guid(guidValue);

            var messageFailure = deserializedResponse.Failure;
            throw new CreateCallResponseException(string.IsNullOrEmpty(messageFailure)
                ? "missing call id in the response body"
                : messageFailure);
        }

        public List<Call> GetCalls()
        {
            // TODO sanity check
            return FindAll<Call>().ToList();
        }

        public Call GetCall(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("id must not be null");

            return FindById<Call>(id.ToString());
        }

        public void DeleteCall(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("id must not be null");

            var restRequest = AuthenticateRequest();
            restRequest.Resource = Resource + "/{uuid}";
            restRequest.Method = Method.DELETE;
            restRequest.AddUrlSegment("uuid", id.ToString());

            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);

            var deserializedResponse =
                JsonConvert.DeserializeObject<ResponseCall>(response.Content);
            if (deserializedResponse.Failure != null)
                throw new DeleteCallResponseException(deserializedResponse.Failure);
        }

        public class ResponseCall
        {
            [JsonProperty("ok")] public string Ok { get; set; }

            [JsonProperty("failure")] public string Failure { get; set; }
        }
    }
}