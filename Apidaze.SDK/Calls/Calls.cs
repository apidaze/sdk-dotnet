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
    /// <summary>
    /// Class Calls.
    /// Implements the <see cref="APIdaze.SDK.Base.BaseApiClient" />
    /// Implements the <see cref="APIdaze.SDK.Calls.ICalls" />
    /// </summary>
    /// <seealso cref="APIdaze.SDK.Base.BaseApiClient" />
    /// <seealso cref="APIdaze.SDK.Calls.ICalls" />
    public class Calls : BaseApiClient, ICalls
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Calls" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="credentials">The credentials.</param>
        public Calls(IRestClient client, Credentials credentials) : base(client, credentials)
        {
        }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <value>The resource.</value>
        protected override string Resource => "/calls";

        /// <summary>
        /// Creates the call.
        /// </summary>
        /// <param name="callerId">The caller identifier.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="callType">Type of the call.</param>
        /// <returns>Guid.</returns>
        /// <exception cref="ArgumentException">origin must not be null or empty</exception>
        /// <exception cref="ArgumentException">destination must not be null or empty</exception>
        /// <exception cref="CreateCallResponseException"></exception>
        /// <exception cref="System.ArgumentException">origin must not be null or empty</exception>
        /// <exception cref="System.ArgumentException">destination must not be null or empty</exception>
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

        /// <summary>
        /// Gets the calls.
        /// </summary>
        /// <returns>List&lt;Call&gt;.</returns>
        public List<Call> GetCalls()
        {
            return FindAll<Call>().ToList();
        }

        /// <summary>
        /// Gets the call.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Call.</returns>
        /// <exception cref="ArgumentException">id must not be null</exception>
        /// <exception cref="System.ArgumentException">id must not be null</exception>
        public Call GetCall(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("id must not be null");

            return FindById<Call>(id.ToString());
        }

        /// <summary>
        /// Deletes the call.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="ArgumentException">id must not be null</exception>
        /// <exception cref="DeleteCallResponseException"></exception>
        /// <exception cref="System.ArgumentException">id must not be null</exception>
        public void DeleteCall(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("id must not be null");

            var restRequest = AuthenticateRequest();
            restRequest.Resource += "/{uuid}";
            restRequest.Method = Method.DELETE;
            restRequest.AddUrlSegment("uuid", id.ToString());

            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);

            var deserializedResponse =
                JsonConvert.DeserializeObject<ResponseCall>(response.Content);
            if (deserializedResponse.Failure != null)
                throw new DeleteCallResponseException(deserializedResponse.Failure);
        }

        /// <summary>
        /// Class ResponseCall.
        /// </summary>
        public class ResponseCall
        {
            /// <summary>
            /// Gets or sets the ok.
            /// </summary>
            /// <value>The ok.</value>
            [JsonProperty("ok")] public string Ok { get; set; }

            /// <summary>
            /// Gets or sets the failure.
            /// </summary>
            /// <value>The failure.</value>
            [JsonProperty("failure")] public string Failure { get; set; }
        }
    }
}