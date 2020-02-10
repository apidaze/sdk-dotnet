using System;

namespace Apidaze.SDK.Exception
{
    public class DeleteCallResponseException : SystemException
    {
        public DeleteCallResponseException(string message) : base(message)
        {
        }
    }
}