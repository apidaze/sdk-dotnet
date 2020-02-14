using Newtonsoft.Json;
using System;

namespace Apidaze.SDK.SipUsers
{
    /// <summary>
    /// Class SipUser.
    /// </summary>
    public class SipUser
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")] public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")] public string Name { get; set; }

        /// <summary>
        /// Gets or sets the caller identifier.
        /// </summary>
        /// <value>The caller identifier.</value>
        [JsonProperty("callerid")] public CallerIdentity CallerId { get; set; }

        /// <summary>
        /// Gets or sets the sip.
        /// </summary>
        /// <value>The sip.</value>
        [JsonProperty("sip")] public Sip Sip { get; set; }

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