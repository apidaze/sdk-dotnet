using System;
using Apidaze.SDK.Base;
using Newtonsoft.Json;
using RestSharp;

namespace Apidaze.SDK.Messages
{
    /// <summary>
    /// Class Message.
    /// Implements the <see cref="Apidaze.SDK.Base.BaseApiClient" />
    /// Implements the <see cref="Apidaze.SDK.Messages.IMessage" />
    /// </summary>
    /// <seealso cref="Apidaze.SDK.Base.BaseApiClient" />
    /// <seealso cref="Apidaze.SDK.Messages.IMessage" />
    public class Message : BaseApiClient, IMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="credentials">The credentials.</param>
        public Message(IRestClient client, Credentials credentials) : base(client, credentials)
        {
        }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <value>The resource.</value>
        protected override string Resource => "/sms/send";

        /// <summary>
        /// Sends the text message.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="bodyMessage">The body message.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentException">body must not be null or empty</exception>
        /// <exception cref="System.ArgumentException">body must not be null or empty</exception>
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

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="bodyMessage">The body message.</param>
        /// <returns>RestRequest.</returns>
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