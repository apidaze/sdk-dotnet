using System;
using Newtonsoft.Json;

namespace Apidaze.SDK.ExternalScripts
{
    /// <summary>
    /// Class ExternalScript.
    /// </summary>
    public class ExternalScript
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")] public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")] public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [JsonProperty("url")] public Uri Url { get; set; }

        /// <summary>
        /// Gets or sets the SMS URL.
        /// </summary>
        /// <value>The SMS URL.</value>
        [JsonProperty("sms_url")] public Uri SmsUrl { get; set; }

        /// <summary>
        /// Gets or sets the reseller customer identifier.
        /// </summary>
        /// <value>The reseller customer identifier.</value>
        [JsonProperty("reseller_cust_id")] public long ResellerCustomerId { get; set; }

        /// <summary>
        /// Gets or sets the dev customer identifier.
        /// </summary>
        /// <value>The dev customer identifier.</value>
        [JsonProperty("dev_cust_id")] public long DevCustomerId { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        [JsonProperty("created_at")] public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// <value>The updated at.</value>
        [JsonProperty("updated_at")] public DateTime UpdatedAt { get; set; }
    }
}