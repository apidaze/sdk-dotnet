namespace Apidaze.SDK.Exception
{
    /// <summary>
    ///     Class ApidazeRestException.
    ///     Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ApidazeRestException : System.Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ApidazeRestException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="statusCode">The status code.</param>
        public ApidazeRestException(string message, uint statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        ///     Gets or sets the status code.
        /// </summary>
        /// <value>The status code.</value>
        public uint StatusCode { get; set; }
    }
}