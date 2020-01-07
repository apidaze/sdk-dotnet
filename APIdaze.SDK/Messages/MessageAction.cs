using APIdaze.SDK.Base;
using RestSharp;

namespace APIdaze.SDK.Messages
{
    internal class MessageAction : IMessageAction
    {
        private readonly Credentials credentials;
        private readonly string url;

        public MessageAction(Credentials credentials, string url)
        {
            this.credentials = credentials;
            this.url = url;
        }

        public string SendTextMessage(PhoneNumber from, PhoneNumber to, string bodyMessage)
        {
            return new MessageApi(new RestClient(url), credentials).SendTextMessage(from, to, bodyMessage);
        }
    }
}