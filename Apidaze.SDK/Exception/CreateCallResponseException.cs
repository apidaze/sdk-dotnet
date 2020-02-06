using System;

namespace APIdaze.SDK.Exception
{
    public class CreateCallResponseException : SystemException
    {
        public CreateCallResponseException(string message) : base(message)
        {
        }
    }
}