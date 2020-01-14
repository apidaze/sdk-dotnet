using System;
using System.Collections.Generic;
using System.Text;
using APIdaze.SDK.Calls;
using Newtonsoft.Json;

namespace APIdaze.SDK.CdrHttpHandlers
{
    public class CdrHttpHandler
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("format")] public Format Format { get; set; }

        [JsonProperty("uri")] public Uri Uri { get; set; }

        [JsonProperty("call_leg")] public CallLeg CallLeg { get; set; }

        [JsonProperty("created_at")] public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")] public DateTime UpdatedAt { get; set; }
    }

    public class CallLeg
    {
        [JsonProperty("inbound")]
        public string INBOUND { get; set; }

        [JsonProperty("outbound")]
        public string OUTBOUND { get; set; }

        [JsonProperty("xml")]
        public string BOTH { get; set; }
    }

    public class Format
    {
        [JsonProperty("regular")]
        public string REGULAR { get; set; }

        [JsonProperty("json")]
        public string JSON { get; set; }

        [JsonProperty("xml")]
        public string XML { get; set; }
    }
}
