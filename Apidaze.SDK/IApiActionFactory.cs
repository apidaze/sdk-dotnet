using Apidaze.SDK.Applications;
using Apidaze.SDK.Calls;
using Apidaze.SDK.CdrHttpHandlers;
using Apidaze.SDK.ExternalScripts;
using Apidaze.SDK.Validate;
using Apidaze.SDK.Messages;
using Apidaze.SDK.Recordings;

/// <summary>
///  ApiActionFactory object that will create Apidaze API request objects on a per-application.
/// </summary>
namespace Apidaze.SDK
{
    public interface IApiActionFactory
    {
        /// <summary>Creates the message API.</summary>
        IMessage CreateMessageApi();

        /// <summary>Creates the credentials validator API.</summary>
        /// <returns></returns>
        ICredentialsValidator CreateCredentialsValidatorApi();

        /// <summary>Creates the calls API.</summary>
        /// <returns></returns>
        ICalls CreateCallsApi();

        /// <summary>Creates the applications API.</summary>
        /// <returns></returns>
        IApplications CreateApplicationsApi();

        /// <summary>Creates the CDR HTTP handlers API.</summary>
        /// <returns></returns>
        ICdrHttpHandlers CreateCdrHttpHandlersApi();

        /// <summary>Creates the recordings API.</summary>
        /// <returns></returns>
        IRecordings CreateRecordingsApi();

        /// <summary>Creates the external scripts API.</summary>
        /// <returns></returns>
        IExternalScripts CreateExternalScriptsApi();
    }
}