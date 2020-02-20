namespace Apidaze.SDK.Exception
{
    /// <summary>
    ///     Class InvalidPhoneNumberException.
    ///     Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InvalidPhoneNumberException : System.Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidPhoneNumberException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InvalidPhoneNumberException(string message) : base(message)
        {
        }
    }
}