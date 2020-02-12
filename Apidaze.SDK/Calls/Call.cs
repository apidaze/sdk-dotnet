using System;
using System.Collections.Generic;
using System.Text;
using Apidaze.SDK.Base;
using Newtonsoft.Json;

namespace Apidaze.SDK.Calls
{
    /// <summary>
    /// Class Call.
    /// </summary>
    public class Call
    {
        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        /// <value>The UUID.</value>
        [JsonProperty("uuid")] public Guid Uuid { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>The created.</value>
        [JsonProperty("created")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd HH:mm:ss")]
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Gets or sets the name of the caller identifier.
        /// </summary>
        /// <value>The name of the caller identifier.</value>
        [JsonProperty("cid_name")] public string CallerIdName { get; set; }

        /// <summary>
        /// Gets or sets the caller identifier number.
        /// </summary>
        /// <value>The caller identifier number.</value>
        [JsonProperty("cid_num")] public string CallerIdNumber { get; set; }

        /// <summary>
        /// Gets or sets the destination.
        /// </summary>
        /// <value>The destination.</value>
        [JsonProperty("dest")] public string Destination { get; set; }

        [JsonProperty("callstate")] public string CallState { get; set; }

        /// <summary>
        /// Gets or sets the call UUID.
        /// </summary>
        /// <value>The call UUID.</value>
        [JsonProperty("call_uuid")] public string CallUuid { get; set; }

        /// <summary>
        /// Gets or sets the caller identifier.
        /// </summary>
        /// <value>The caller identifier.</value>
        [JsonProperty("callerid")] public string CallerId { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [JsonProperty("URL")] public string URL { get; set; }

        /// <summary>
        /// Gets or sets the work tag.
        /// </summary>
        /// <value>The work tag.</value>
        [JsonProperty("work_tag")] public string WorkTag { get; set; }
    }
}