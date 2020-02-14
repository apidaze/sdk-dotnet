using Newtonsoft.Json;

namespace Apidaze.SDK.SipUsers
{
    public class SipUserStatus
    {
        [JsonProperty("uri")] public string Uri { get; set; }

        [JsonProperty("status")] public string Status { get; set; }
    }
}