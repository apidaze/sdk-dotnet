using Newtonsoft.Json;
using System;

namespace Apidaze.SDK.Applications
{
    /// <summary>
    /// Class Application.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id")] public long Id { get; set; }

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>The account identifier.</value>
        [JsonProperty("account_id")] public long AccountId { get; set; }

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>The application identifier.</value>
        [JsonProperty("application_id")] public string ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        /// <value>The API key.</value>
        [JsonProperty("api_key")] public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the API secret.
        /// </summary>
        /// <value>The API secret.</value>
        [JsonProperty("api_secret")] public string ApiSecret { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")] public string Name { get; set; }

        /// <summary>
        /// Gets or sets the fs address.
        /// </summary>
        /// <value>The fs address.</value>
        [JsonProperty("fs_address")] public string FsAddress { get; set; }

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