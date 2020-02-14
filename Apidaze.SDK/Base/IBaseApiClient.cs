using System.Collections.Generic;

namespace Apidaze.SDK.Base
{
    /// <summary>
    /// Interface IBaseApiClient
    /// </summary>
    internal interface IBaseApiClient
    {
        /// <summary>
        /// Creates the specified request parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestParams">The request parameters.</param>
        /// <returns>T.</returns>
        T Create<T>(Dictionary<string, string> requestParams, string resource = "") where T : new();

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> FindAll<T>();

        /// <summary>
        /// Finds the by parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        IEnumerable<T> FindByParameter<T>(string name, string value) where T : new();

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>T.</returns>
        T FindById<T>(string id, string resource = "") where T : new();

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="requestParams">The request parameters.</param>
        /// <returns>T.</returns>
        T Update<T>(string id, Dictionary<string, string> requestParams) where T : new();

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(string id);
    }
}