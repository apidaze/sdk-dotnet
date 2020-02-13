using System;
using System.Collections.Generic;

namespace Apidaze.SDK.ExternalScripts
{
    /// <summary>
    /// Interface IExternalScripts
    /// </summary>
    public interface IExternalScripts
    {
        /// <summary>
        /// Gets the external scripts.
        /// </summary>
        /// <returns>List&lt;ExternalScript&gt;.</returns>
        List<ExternalScript> GetExternalScripts();

        /// <summary>
        /// Gets the external script.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ExternalScript.</returns>
        ExternalScript GetExternalScript(long id);

        /// <summary>
        /// Updates the external script.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="url">The URL.</param>
        /// <returns>ExternalScript.</returns>
        ExternalScript UpdateExternalScript(long id, string name, Uri url);

        /// <summary>
        /// Updates the external script URL.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="url">The URL.</param>
        /// <returns>ExternalScript.</returns>
        ExternalScript UpdateExternalScriptUrl(long id, Uri url);
    }
}