using System;

namespace Apidaze.SDK.Calls
{
    public class DeleteCallResponseException : SystemException
    {
        public DeleteCallResponseException(string message) : base(message)
        {
        }
    }
}