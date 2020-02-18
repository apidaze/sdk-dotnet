using System.Collections.Generic;

namespace Apidaze.SDK.Applications
{
    /// <summary>
    /// Interface IApplications
    /// </summary>
    public interface IApplications
    {
        /// <summary>
        /// Gets the applications.
        /// </summary>
        /// <returns>List&lt;Application&gt;.</returns>
        List<Application> GetApplications();

        /**
         * Returns an application details retrieved by application id.
         * @param id id of the application to fetch
         * @return a list containing one element with application details if it exists, otherwise an empty list
         * @throws IOException
         * @throws HttpResponseException
         */
        /// <summary>
        /// Gets the applications by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>List&lt;Application&gt;.</returns>
        List<Application> GetApplicationsById(long id);

        /**
         * Returns an application details retrieved by api key.
         * @param apiKey api key of the application to fetch
         * @return a list containing one element with application details if it exists, otherwise an empty list
         * @throws IOException
         * @throws HttpResponseException
         */
        /// <summary>
        /// Gets the applications by API key.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <returns>List&lt;Application&gt;.</returns>
        List<Application> GetApplicationsByApiKey(string apiKey);

        /**
         * Returns an application details retrieved by api key.
         * @param name the name of the application to fetch
         * @return a list containing one element with application details if it exists, otherwise an empty list
         * @throws IOException
         * @throws HttpResponseException
         */
        /// <summary>
        /// Gets the name of the applications by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>List&lt;Application&gt;.</returns>
        List<Application> GetApplicationsByName(string name);

        /// <summary>
        /// Gets the application action by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IApiActionFactory.</returns>
        IApiActionFactory GetApplicationActionById(long id);

        /// <summary>
        /// Gets the application action by API key.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <returns>IApiActionFactory.</returns>
        IApiActionFactory GetApplicationActionByApiKey(string apiKey);

        /// <summary>
        /// Gets the name of the application action by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>IApiActionFactory.</returns>
        IApiActionFactory GetApplicationActionByName(string name);
    }
}