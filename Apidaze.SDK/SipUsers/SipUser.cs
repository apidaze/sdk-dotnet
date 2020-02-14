using Newtonsoft.Json;
using System;

namespace Apidaze.SDK.SipUsers
{
    public class SipUser
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("callerid")] public CallerIdentity CalledId { get; set; }

        [JsonProperty("sip")] public Sip Sip { get; set; }

        [JsonProperty("created_at")] public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")] public DateTime UpdatedAt { get; set; }
    }
}