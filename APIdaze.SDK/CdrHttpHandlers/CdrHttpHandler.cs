using System;
using System.Collections.Generic;
using System.Text;
using APIdaze.SDK.Calls;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace APIdaze.SDK.CdrHttpHandlers
{
    public class CdrHttpHandler
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("format")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Format Format { get; set; }

        [JsonProperty("uri")] public Uri Uri { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("call_leg")] public CallLeg CallLeg { get; set; }

        [JsonProperty("created_at")] public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")] public DateTime UpdatedAt { get; set; }
    }
 

    public enum CallLeg
    {
        Inbound,
        Outbound,
        Xml
    }

    public enum Format
    {
        Regular,
        Json,
        Xml
    }
}