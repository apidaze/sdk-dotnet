using System;

namespace Apidaze.SDK.Calls
{
    /// <summary>
    ///     Class DeleteCallResponseException.
    ///     Implements the <see cref="System.SystemException" />
    /// </summary>
    /// <seealso cref="System.SystemException" />
    public class DeleteCallResponseException : SystemException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DeleteCallResponseException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DeleteCallResponseException(string message) : base(message)
        {
        }
    }
}