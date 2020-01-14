using APIdaze.SDK.Applications;
using APIdaze.SDK.Calls;
using APIdaze.SDK.CdrHttpHandlers;
using APIdaze.SDK.Validates;
using APIdaze.SDK.Messages;

namespace APIdaze.SDK
{
    public interface IApiActionFactory
    {
        IMessage CreateMessageApi();

        ICredentialsValidator CreateCredentialsValidatorApi();

        ICalls CreateCallsApi();

        IApplications CreateApplicationsApi();

        ICdrHttpHandlers CreateCdrHttpHandlersApi();
    }
}