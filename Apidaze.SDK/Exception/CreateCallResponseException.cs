using System;

namespace Apidaze.SDK.Exception
{
    public class CreateCallResponseException : SystemException
    {
        public CreateCallResponseException(string message) : base(message)
        {
        }
    }
}