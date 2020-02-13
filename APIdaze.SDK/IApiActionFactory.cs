﻿using Apidaze.SDK.Applications;
using Apidaze.SDK.Calls;
using Apidaze.SDK.CdrHttpHandlers;
using Apidaze.SDK.ExternalScripts;
using Apidaze.SDK.Messages;
using Apidaze.SDK.Recordings;
using Apidaze.SDK.Validate;

/// <summary>
///  ApiActionFactory object that will create Apidaze API request objects on a per-application.
/// </summary>
namespace Apidaze.SDK
{
    /// <summary>
    /// Interface IApiActionFactory
    /// </summary>
    public interface IApiActionFactory
    {
        /// <summary>
        /// Creates the message API.
        /// </summary>
        /// <returns>IMessage.</returns>
        IMessage CreateMessageApi();

        /// <summary>
        /// Creates the credentials validator API.
        /// </summary>
        /// <returns>ICredentialsValidator.</returns>
        ICredentialsValidator CreateCredentialsValidatorApi();

        /// <summary>
        /// Creates the calls API.
        /// </summary>
        /// <returns>ICalls.</returns>
        ICalls CreateCallsApi();

        /// <summary>
        /// Creates the applications API.
        /// </summary>
        /// <returns>IApplications.</returns>
        IApplications CreateApplicationsApi();

        /// <summary>
        /// Creates the CDR HTTP handlers API.
        /// </summary>
        /// <returns>ICdrHttpHandlers.</returns>
        ICdrHttpHandlers CreateCdrHttpHandlersApi();

        /// <summary>
        /// Creates the recordings API.
        /// </summary>
        /// <returns>IRecordings.</returns>
        IRecordings CreateRecordingsApi();

        /// <summary>
        /// Creates the external scripts API.
        /// </summary>
        /// <returns>IExternalScripts.</returns>
        IExternalScripts CreateExternalScriptsApi();
    }
}