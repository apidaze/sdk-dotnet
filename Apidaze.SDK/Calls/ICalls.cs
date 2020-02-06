using System;
using System.Collections.Generic;
using System.Text;
using Apidaze.SDK.Messages;
using Microsoft.VisualBasic;

namespace Apidaze.SDK.Calls
{
    public interface ICalls
    {
        Guid CreateCall(PhoneNumber callerId, string origin, string destination, string callType);

        List<Call> GetCalls();

        Call GetCall(Guid id);

        void DeleteCall(Guid id);
    }
}