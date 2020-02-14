using Newtonsoft.Json;

namespace Apidaze.SDK.SipUsers
{
    public class Sip
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}