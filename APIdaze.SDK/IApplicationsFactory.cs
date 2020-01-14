using APIdaze.SDK.Applications;

namespace APIdaze.SDK
{
    public interface IApplicationsFactory
    {
        IApplications CreateApplicationsApi();
    }
}
