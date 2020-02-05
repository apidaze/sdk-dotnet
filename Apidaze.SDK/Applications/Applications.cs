﻿using APIdaze.SDK.Base;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIdaze.SDK.Applications
{
    /// <summary>
    /// Class Applications.
    /// Implements the <see cref="APIdaze.SDK.Base.BaseApiClient" />
    /// Implements the <see cref="APIdaze.SDK.Applications.IApplications" />
    /// </summary>
    /// <seealso cref="APIdaze.SDK.Base.BaseApiClient" />
    /// <seealso cref="APIdaze.SDK.Applications.IApplications" />
    public class Applications : BaseApiClient, IApplications
    {
        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <value>The resource.</value>
        protected override string Resource => "/applications";

        /// <summary>
        /// Initializes a new instance of the <see cref="Applications"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="credentials">The credentials.</param>
        public Applications(IRestClient client, Credentials credentials) : base(client, credentials)
        {
        }

        /// <summary>
        /// Gets the applications.
        /// </summary>
        /// <returns>List&lt;Application&gt;.</returns>
        public List<Application> GetApplications()
        {
            return FindAll<Application>().ToList();
        }

        /// <summary>
        /// Gets the applications by API key.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <returns>List&lt;Application&gt;.</returns>
        /// <exception cref="ArgumentException">apiKey must not be null</exception>
        /// * Returns an application details retrieved by api key.
        /// * @param apiKey api key of the application to fetch
        /// * @return a list containing one element with application details if it exists, otherwise an empty list
        /// * @throws IOException
        /// * @throws HttpResponseException
        /// */
        public List<Application> GetApplicationsByApiKey(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException("apiKey must not be null");
            }
            return FindByParameter<Application>("api_key", apiKey).ToList();
        }

        /// <summary>
        /// Gets the applications by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>List&lt;Application&gt;.</returns>
        /// * Returns an application details retrieved by application id.
        /// * @param id id of the application to fetch
        /// * @return a list containing one element with application details if it exists, otherwise an empty list
        /// * @throws IOException
        /// * @throws HttpResponseException
        /// */
        public List<Application> GetApplicationsById(long id)
        {
            return FindByParameter<Application>("api_id", id.ToString()).ToList();
        }

        /// <summary>
        /// Gets the name of the applications by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>List&lt;Application&gt;.</returns>
        /// <exception cref="ArgumentException">apiKey must not be null</exception>
        /// * Returns an application details retrieved by api key.
        /// * @param name the name of the application to fetch
        /// * @return a list containing one element with application details if it exists, otherwise an empty list
        /// * @throws IOException
        /// * @throws HttpResponseException
        /// */
        public List<Application> GetApplicationsByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("apiKey must not be null");
            }
            return FindByParameter<Application>("api_name", name).ToList();
        }
    }
}