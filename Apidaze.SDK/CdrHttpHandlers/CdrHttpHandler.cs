using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Apidaze.SDK.CdrHttpHandlers
{
    public class CdrHttpHandler
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("format")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Format Format { get; set; }

        [JsonProperty("url")] public Uri Url { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("call_leg")]
        public CallLeg CallLeg { get; set; }

        [JsonProperty("created_at")] public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")] public DateTime UpdatedAt { get; set; }
    }
}