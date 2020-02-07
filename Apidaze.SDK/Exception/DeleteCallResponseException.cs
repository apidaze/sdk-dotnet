using System;

namespace APIdaze.SDK.Exception
{
    public class DeleteCallResponseException : SystemException
    {
        public DeleteCallResponseException(string message) : base(message)
        {
        }
    }
}