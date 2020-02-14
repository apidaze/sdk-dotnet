using Newtonsoft.Json;

namespace Apidaze.SDK.SipUsers
{
    public class CallerIdentity
    {
        [JsonProperty("outboundCallerIdName")] public string OutboundCallerIdName { get; set; }

        [JsonProperty("outboundCallerIdNumber")] public string OutboundCallerIdNumber { get; set; }

        [JsonProperty("internalCallerIdName")] public string InternalCallerIdName { get; set; }

        [JsonProperty("internalCallerIdNumber")] public string InternalCallerIdNumber { get; set; }
    }
}