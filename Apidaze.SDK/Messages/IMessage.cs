namespace Apidaze.SDK.Messages
{
    /// <summary>
    ///     Interface IMessage
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        ///     Sends the text message.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="bodyMessage">The body message.</param>
        /// <returns>System.String.</returns>
        string SendTextMessage(PhoneNumber from, PhoneNumber to, string bodyMessage);
    }
}