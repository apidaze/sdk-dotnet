using Newtonsoft.Json;
using System;

namespace Apidaze.SDK.ExternalScripts
{
    public class ExternalScript
    {
        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("url")]
         public Uri Url { get; set; }

        [JsonProperty("sms_url")]
         public Uri SmsUrl { get; set; }

        [JsonProperty("reseller_cust_id")] public long ResellerCustomerId { get; set; }
       
        [JsonProperty("dev_cust_id")] public long DevCustomerId { get; set; }

        [JsonProperty("created_at")] public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")] public DateTime UpdatedAt { get; set; }
    }
}
