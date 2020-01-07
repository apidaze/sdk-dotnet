using System;
using System.Collections.Generic;
using System.Text;
using APIdaze.SDK.Applications;

namespace APIdaze.SDK
{
    public interface IApplicationsFactory
    {
        IApplications CreateApplicationsApi();
    }
}
