using Apidaze.SDK.Applications;
using Apidaze.SDK.Calls;
using Apidaze.SDK.CdrHttpHandlers;
using Apidaze.SDK.ExternalScripts;
using Apidaze.SDK.Validates;
using Apidaze.SDK.Messages;
using Apidaze.SDK.Recordings;

namespace Apidaze.SDK
{
    public interface IApiActionFactory
    {
        IMessage CreateMessageApi();

        ICredentialsValidator CreateCredentialsValidatorApi();

        ICalls CreateCallsApi();

        IApplications CreateApplicationsApi();

        ICdrHttpHandlers CreateCdrHttpHandlersApi();

        IRecordings CreateRecordingsApi();

        IExternalScripts CreateExternalScriptsApi();
    }
}