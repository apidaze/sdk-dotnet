using System;
using System.Collections.Generic;
using System.Text;
using Apidaze.SDK.Calls;

namespace Apidaze.SDK.Tests.Unit.Calls
{
    /// <summary>
    /// Class CallBuilder.
    /// </summary>
    public class CallBuilder
    {
        /// <summary>
        /// The call
        /// </summary>
        private Call _call;
        /// <summary>
        /// Creates the builder.
        /// </summary>
        /// <returns>CallBuilder.</returns>
        public CallBuilder CreateBuilder()
        {
            _call = new Call();
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>Call.</returns>
        public Call Build()
        {
            return _call;
        }

        /// <summary>
        /// Withes the UUID.
        /// </summary>
        /// <param name="uuid">The UUID.</param>
        /// <returns>CallBuilder.</returns>
        public CallBuilder WithUuid(Guid uuid)
        {
            _call.Uuid = uuid;
            return this;
        }

        /// <summary>
        /// Withes the created.
        /// </summary>
        /// <param name="dateTimeOffset">The date time offset.</param>
        /// <returns>CallBuilder.</returns>
        public CallBuilder WithCreated(DateTimeOffset dateTimeOffset)
        {
            _call.Created = dateTimeOffset;
            return this;
        }

        /// <summary>
        /// Withes the name of the caller identifier.
        /// </summary>
        /// <param name="callerIdName">Name of the caller identifier.</param>
        /// <returns>CallBuilder.</returns>
        public CallBuilder WithCallerIdName(string callerIdName)
        {
            _call.CallerIdName = callerIdName;
            return this;
        }

        /// <summary>
        /// Withes the caller identifier number.
        /// </summary>
        /// <param name="callerIdNumber">The caller identifier number.</param>
        /// <returns>CallBuilder.</returns>
        public CallBuilder WithCallerIdNumber(string callerIdNumber)
        {
            _call.CallerIdNumber = callerIdNumber;
            return this;
        }

        /// <summary>
        /// Withes the destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <returns>CallBuilder.</returns>
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

        /// <summary>
        /// Withes the call UUID.
        /// </summary>
        /// <param name="callUuid">The call UUID.</param>
        /// <returns>CallBuilder.</returns>
        public CallBuilder WithCallUuid(string callUuid)
        {
            _call.CallUuid = callUuid;
            return this;
        }
    }
}