using System;
using System.Collections.Generic;
using System.Text;
using Apidaze.SDK.Base;
using Newtonsoft.Json;

namespace Apidaze.SDK.Calls
{
    public class Call
    {
        [JsonProperty("uuid")] public Guid Uuid { get; set; }

        [JsonProperty("created")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd HH:mm:ss")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("cid_name")] public string CallerIdName { get; set; }

        [JsonProperty("cid_num")] public string CallerIdNumber { get; set; }

        [JsonProperty("dest")] public string Destination { get; set; }

        [JsonProperty("callstate")] public string CallState { get; set; }

        [JsonProperty("call_uuid")] public string CallUuid { get; set; }

        [JsonProperty("callerid")] public string CallerId { get; set; }

        [JsonProperty("URL")] public string URL { get; set; }

        [JsonProperty("work_tag")] public string WorkTag { get; set; }
    }
}