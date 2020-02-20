using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Apidaze.SDK.CdrHttpHandlers
{
    /// <summary>
    ///     Class CdrHttpHandler.
    /// </summary>
    public class CdrHttpHandler
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the format.
        /// </summary>
        /// <value>The format.</value>
        [JsonProperty("format")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Format Format { get; set; }

        [JsonProperty("url")] public Uri Url { get; set; }

        /// <summary>
        ///     Gets or sets the call leg.
        /// </summary>
        /// <value>The call leg.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("call_leg")]
        public CallLeg CallLeg { get; set; }

        /// <summary>
        ///     Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     Gets or sets the updated at.
        /// </summary>
        /// <value>The updated at.</value>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}