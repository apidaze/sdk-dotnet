using System;

namespace Apidaze.SDK.Calls
{
    public class CreateCallResponseException : SystemException
    {
        public CreateCallResponseException(string message) : base(message)
        {
        }
    }
}