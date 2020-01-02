using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Apidaze.SDK.Base;

namespace Apidaze.SDK.Applications
{
    public class Application
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("account_id")]
        public long AccountId { get; set; }

        [JsonProperty("application_id")]
        public string ApplicationId { get; set; }

        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        [JsonProperty("api_secret")]
        public string ApiSecret { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fs_address")]
        public string FsAddress { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonIgnore]
        public Credentials Credentials => new Credentials(ApiKey, ApiSecret);
    }
}
