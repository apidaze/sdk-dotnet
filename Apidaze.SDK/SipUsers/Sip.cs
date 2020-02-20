using Newtonsoft.Json;

namespace Apidaze.SDK.SipUsers
{
    /// <summary>
    ///     Class Sip.
    /// </summary>
    public class Sip
    {
        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}