using APIdaze.SDK.Applications;
using APIdaze.SDK.Calls;
using APIdaze.SDK.CdrHttpHandlers;
using APIdaze.SDK.ExternalScripts;
using APIdaze.SDK.Messages;
using APIdaze.SDK.Recordings;
using APIdaze.SDK.Validates;

/// <summary>
///  ApiActionFactory object that will create Apidaze API request objects on a per-application.
/// </summary>
namespace APIdaze.SDK
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