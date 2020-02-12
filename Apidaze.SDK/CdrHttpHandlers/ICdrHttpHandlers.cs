using System;
using System.Collections.Generic;

namespace Apidaze.SDK.CdrHttpHandlers
{
    /// <summary>
    /// Interface ICdrHttpHandlers
    /// </summary>
    public interface ICdrHttpHandlers
    {
        /// <summary>
        /// Gets the CDR HTTP handlers.
        /// </summary>
        /// <returns>List&lt;CdrHttpHandler&gt;.</returns>
        List<CdrHttpHandler> GetCdrHttpHandlers();

        /// <summary>
        /// Creates the CDR HTTP handler.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="url">The URL.</param>
        /// <returns>CdrHttpHandler.</returns>
        CdrHttpHandler CreateCdrHttpHandler(string name, Uri url);

        /// <summary>
        /// Updates the CDR HTTP handler.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="url">The URL.</param>
        /// <returns>CdrHttpHandler.</returns>
        CdrHttpHandler UpdateCdrHttpHandler(long id, string name, Uri url);

        /// <summary>
        /// Updates the name of the CDR HTTP handler.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns>CdrHttpHandler.</returns>
        CdrHttpHandler UpdateCdrHttpHandlerName(long id, string name);

        /// <summary>
        /// Updates the CDR HTTP handler URL.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="url">The URL.</param>
        /// <returns>CdrHttpHandler.</returns>
        CdrHttpHandler UpdateCdrHttpHandlerUrl(long id, Uri url);

        /// <summary>
        /// Deletes the CDR HTTP handler.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteCdrHttpHandler(long id);
    }
}