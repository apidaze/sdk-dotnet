using System;
using System.Collections.Generic;
using System.Text;
using APIdaze.SDK.Messages;
using Microsoft.VisualBasic;

namespace APIdaze.SDK.Calls
{
    public interface ICalls
    {
        Guid CreateCall(PhoneNumber callerId, string origin, string destination, string callType);

        List<Call> GetCalls();

        Call GetCall(Guid id);

        void DeleteCall(Guid id);

    }

    public class CallType
    {
        public static string NUMBER => "number";
        public static string SIP_ACCOUNT => "sipaccount";
    }

    public class CreateCallResponseException : SystemException
    {
        public CreateCallResponseException(string message) : base(message) {}
    }

    public class DeleteCallResponseException : SystemException
    {
        public DeleteCallResponseException(string message) : base(message) {}
    }
}
