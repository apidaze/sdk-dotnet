namespace Apidaze.SDK.Exception
{
    /// <summary>
    ///     Class ApidazeCredentialsException.
    ///     Implements the <see cref="Apidaze.SDK.Exception.ApidazeRestException" />
    /// </summary>
    /// <seealso cref="Apidaze.SDK.Exception.ApidazeRestException" />
    public class ApidazeCredentialsException : ApidazeRestException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ApidazeCredentialsException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="statusCode">The status code.</param>
        public ApidazeCredentialsException(string message, uint statusCode = 400) : base(message, statusCode)
        {
        }
    }
}