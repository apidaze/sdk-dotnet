using Apidaze.SDK.Messages;
using System;
using System.Collections.Generic;

namespace Apidaze.SDK.Calls
{
    /// <summary>
    /// Interface ICalls
    /// </summary>
    public interface ICalls
    {
        /// <summary>
        /// Creates the call.
        /// </summary>
        /// <param name="callerId">The caller identifier.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="callType">Type of the call.</param>
        /// <returns>Guid.</returns>
        Guid CreateCall(PhoneNumber callerId, string origin, string destination, string callType);

        /// <summary>
        /// Gets the calls.
        /// </summary>
        /// <returns>List&lt;Call&gt;.</returns>
        List<Call> GetCalls();

        /// <summary>
        /// Gets the call.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Call.</returns>
        Call GetCall(Guid id);

        /// <summary>
        /// Deletes the call.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteCall(Guid id);
    }
}