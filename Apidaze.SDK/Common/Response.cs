using Newtonsoft.Json;

namespace Apidaze.SDK.Common
{
    /// <summary>
    ///     Class Response.
    /// </summary>
    public class Response
    {
        /// <summary>
        ///     Gets or sets the ok.
        /// </summary>
        /// <value>The ok.</value>
        [JsonProperty("ok")]
        public string Ok { get; set; }

        /// <summary>
        ///     Gets or sets the failure.
        /// </summary>
        /// <value>The failure.</value>
        [JsonProperty("failure")]
        public string Failure { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}