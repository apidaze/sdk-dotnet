using System;

namespace APIdaze.SDK.Calls
{
    public class CreateCallResponseException : SystemException
    {
        public CreateCallResponseException(string message) : base(message)
        {
        }
    }
}