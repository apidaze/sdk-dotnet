using System;
using System.Collections.Generic;
using System.Text;
using Apidaze.SDK.Calls;

namespace Apidaze.SDK.Tests.Unit.Calls
{
    public class CallBuilder
    {
        private Call _call;
        public CallBuilder CreateBuilder()
        {
            _call = new Call();
            return this;
        }

        public Call Build()
        {
            return _call;
        }

        public CallBuilder WithUuid(Guid uuid)
        {
            _call.Uuid = uuid;
            return this;
        }

        public CallBuilder WithCreated(DateTimeOffset dateTimeOffset)
        {
            _call.Created = dateTimeOffset;
            return this;
        }

        public CallBuilder WithCallerIdName(string callerIdName)
        {
            _call.CallerIdName = callerIdName;
            return this;
        }

        public CallBuilder WithCallerIdNumber(string callerIdNumber)
        {
            _call.CallerIdNumber = callerIdNumber;
            return this;
        }

        public CallBuilder WithDestination(string destination)
        {
            _call.Destination = destination;
            return this;
        }
        
        public CallBuilder WithCallState(string callState)
        {
            _call.CallState = callState;
            return this;
        }

        public CallBuilder WithCallUuid(string callUuid)
        {
            _call.CallUuid = callUuid;
            return this;
        }
    }
}