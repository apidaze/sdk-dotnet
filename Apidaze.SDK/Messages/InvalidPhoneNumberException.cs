namespace APIdaze.SDK.Messages
{
    public class InvalidPhoneNumberException : System.Exception
    {
        public InvalidPhoneNumberException(string message) : base(message)
        {
        }
    }
}