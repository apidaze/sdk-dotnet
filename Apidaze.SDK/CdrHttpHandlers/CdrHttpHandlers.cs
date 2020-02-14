using System;
using System.Collections.Generic;
using System.Linq;
using Apidaze.SDK.Base;
using RestSharp;

namespace Apidaze.SDK.CdrHttpHandlers
{
    /// <summary>
    ///     Class CdrHttpHandlers.
    ///     Implements the <see cref="Apidaze.SDK.Base.BaseApiClient" />
    ///     Implements the <see cref="Apidaze.SDK.CdrHttpHandlers.ICdrHttpHandlers" />
    /// </summary>
    /// <seealso cref="Apidaze.SDK.Base.BaseApiClient" />
    /// <seealso cref="Apidaze.SDK.CdrHttpHandlers.ICdrHttpHandlers" />
    public class CdrHttpHandlers : BaseApiClient, ICdrHttpHandlers
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CdrHttpHandlers" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="credentials">The credentials.</param>
        public CdrHttpHandlers(IRestClient client, Credentials credentials) : base(client, credentials)
        {
        }

        /// <summary>
        ///     Gets the resource.
        /// </summary>
        /// <value>The resource.</value>
        protected override string Resource => "/cdrhttphandlers";

        /// <summary>
        ///     Gets the CDR HTTP handlers.
        /// </summary>
        /// <returns>List&lt;CdrHttpHandler&gt;.</returns>
        public List<CdrHttpHandler> GetCdrHttpHandlers()
        {
            return FindAll<CdrHttpHandler>().ToList();
        }

        /// <summary>
        ///     Creates the CDR HTTP handler.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="url">The URL.</param>
        /// <returns>CdrHttpHandler.</returns>
        /// <exception cref="ArgumentException">origin must not be null or empty</exception>
        /// <exception cref="ArgumentException">destination must not be null or empty</exception>
        public CdrHttpHandler CreateCdrHttpHandler(string name, Uri url)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("name must not be null or empty");
            if (url == null) throw new ArgumentException("url must not be null or empty");

            var requestBody = new Dictionary<string, string> {{"name", name}, {"url", url.ToString()}};
            return Create<CdrHttpHandler>(requestBody);
        }

        /// <summary>
        ///     Updates the CDR HTTP handler.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="url">The URL.</param>
        /// <returns>CdrHttpHandler.</returns>
        /// <exception cref="ArgumentException">origin must not be null or empty</exception>
        /// <exception cref="ArgumentException">destination must not be null or empty</exception>
        public CdrHttpHandler UpdateCdrHttpHandler(long id, string name, Uri url)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("name must not be null or empty");
            if (url == null) throw new ArgumentException("url must not be null or empty");

            var requestBody = new Dictionary<string, string> {{"name", name}, {"url", url.ToString()}};
            return Update<CdrHttpHandler>(id.ToString(), requestBody);
        }

        /// <summary>
        ///     Updates the name of the CDR HTTP handler.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns>CdrHttpHandler.</returns>
        public CdrHttpHandler UpdateCdrHttpHandlerName(long id, string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("name must not be null or empty");

            var requestBody = new Dictionary<string, string> {{"name", name}};
            return Update<CdrHttpHandler>(id.ToString(), requestBody);
        }

        /// <summary>
        ///     Updates the CDR HTTP handler URL.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="url">The URL.</param>
        /// <returns>CdrHttpHandler.</returns>
        public CdrHttpHandler UpdateCdrHttpHandlerUrl(long id, Uri url)
        {
            if (url == null) throw new ArgumentException("url must not be null or empty");

            var requestBody = new Dictionary<string, string> {{"url", url.ToString()}};
            return Update<CdrHttpHandler>(id.ToString(), requestBody);
        }

        /// <summary>
        ///     Deletes the CDR HTTP handler.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteCdrHttpHandler(long id)
        {
            Delete(id.ToString());
        }
    }
}