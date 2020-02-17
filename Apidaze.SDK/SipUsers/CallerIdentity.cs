using Newtonsoft.Json;

namespace Apidaze.SDK.SipUsers
{
    /// <summary>
    /// Class CallerIdentity.
    /// </summary>
    public class CallerIdentity
    {
        /// <summary>
        /// Gets or sets the name of the outbound caller identifier.
        /// </summary>
        /// <value>The name of the outbound caller identifier.</value>
        [JsonProperty("outboundCallerIdName")] public string OutboundCallerIdName { get; set; }

        /// <summary>
        /// Gets or sets the outbound caller identifier number.
        /// </summary>
        /// <value>The outbound caller identifier number.</value>
        [JsonProperty("outboundCallerIdNumber")] public string OutboundCallerIdNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the internal caller identifier.
        /// </summary>
        /// <value>The name of the internal caller identifier.</value>
        [JsonProperty("internalCallerIdName")] public string InternalCallerIdName { get; set; }

        /// <summary>
        /// Gets or sets the internal caller identifier number.
        /// </summary>
        /// <value>The internal caller identifier number.</value>
        [JsonProperty("internalCallerIdNumber")] public string InternalCallerIdNumber { get; set; }
    }
}