using APIdaze.SDK.Applications;
using APIdaze.SDK.CredentialsValidator;
using APIdaze.SDK.Messages;

namespace APIdaze.SDK
{
    public interface IApiActionFactory
    {
        IMessageAction CreateMessage();

        ICredentialsValidator CreateCredentialsValidatorApi();

        IApplications CreateApplicationsApi();
    }
}