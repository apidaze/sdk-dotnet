using Newtonsoft.Json;

namespace Apidaze.SDK.SipUsers
{
    /// <summary>
    ///     Class SipUserStatus.
    /// </summary>
    public class SipUserStatus
    {
        /// <summary>
        ///     Gets or sets the URI.
        /// </summary>
        /// <value>The URI.</value>
        [JsonProperty("uri")]
        public string Uri { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}