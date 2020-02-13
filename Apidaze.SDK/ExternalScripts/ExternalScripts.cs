using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apidaze.SDK.Base;
using RestSharp;

namespace Apidaze.SDK.ExternalScripts
{
    /// <summary>
    /// Class ExternalScripts.
    /// Implements the <see cref="Apidaze.SDK.Base.BaseApiClient" />
    /// Implements the <see cref="Apidaze.SDK.ExternalScripts.IExternalScripts" />
    /// </summary>
    /// <seealso cref="Apidaze.SDK.Base.BaseApiClient" />
    /// <seealso cref="Apidaze.SDK.ExternalScripts.IExternalScripts" />
    public class ExternalScripts : BaseApiClient, IExternalScripts
    {
        /// <summary>
        /// The maximum name length
        /// </summary>
        private readonly int MaxNameLength = 40;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalScripts" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="credentials">The credentials.</param>
        public ExternalScripts(IRestClient client, Credentials credentials) : base(client, credentials)
        {
        }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <value>The resource.</value>
        protected override string Resource => "/externalscripts";

        /// <summary>
        /// Gets the external scripts.
        /// </summary>
        /// <returns>List&lt;ExternalScript&gt;.</returns>
        public List<ExternalScript> GetExternalScripts()
        {
            return FindAll<ExternalScript>().ToList();
        }

        /// <summary>
        /// Gets the external script.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ExternalScript.</returns>
        public ExternalScript GetExternalScript(long id)
        {
            return FindById<ExternalScript>(id.ToString());
        }

        /// <summary>
        /// Updates the external script.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="url">The URL.</param>
        /// <returns>ExternalScript.</returns>
        /// <exception cref="ArgumentException">origin must not be null or empty</exception>
        /// <exception cref="ArgumentException">destination must not be null or empty</exception>
        /// <exception cref="ArgumentException">name: maximum " + MaxNameLength + " characters long</exception>
        /// <exception cref="System.ArgumentException">origin must not be null or empty</exception>
        /// <exception cref="System.ArgumentException">destination must not be null or empty</exception>
        /// <exception cref="System.ArgumentException">name: maximum " + MaxNameLength + " characters long</exception>
        public ExternalScript UpdateExternalScript(long id, string name, Uri url)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("origin must not be null or empty");
            if (url == null) throw new ArgumentException("destination must not be null or empty");
            if (name.Length > MaxNameLength)
                throw new ArgumentException("name: maximum " + MaxNameLength + " characters long");

            var requestBody = new Dictionary<string, string> {{"name", name}, {"url", url.ToString()}};
            return Update<ExternalScript>(id.ToString(), requestBody);
        }

        /// <summary>
        /// Updates the external script URL.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="url">The URL.</param>
        /// <returns>ExternalScript.</returns>
        /// <exception cref="ArgumentException">destination must not be null or empty</exception>
        /// <exception cref="System.ArgumentException">destination must not be null or empty</exception>
        public ExternalScript UpdateExternalScriptUrl(long id, Uri url)
        {
            if (url == null) throw new ArgumentException("destination must not be null or empty");

            var requestBody = new Dictionary<string, string> {{"url", url.ToString()}};

            return Update<ExternalScript>(id.ToString(), requestBody);
        }
    }
}