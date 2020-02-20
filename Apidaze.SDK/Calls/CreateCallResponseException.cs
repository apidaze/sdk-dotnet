using System;

namespace Apidaze.SDK.Calls
{
    /// <summary>
    ///     Class CreateCallResponseException.
    ///     Implements the <see cref="System.SystemException" />
    /// </summary>
    /// <seealso cref="System.SystemException" />
    public class CreateCallResponseException : SystemException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CreateCallResponseException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CreateCallResponseException(string message) : base(message)
        {
        }
    }
}