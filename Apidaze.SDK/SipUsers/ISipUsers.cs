using System.Collections.Generic;

namespace Apidaze.SDK.SipUsers
{
    public interface ISipUsers
    {
        IEnumerable<string> GetSipUsers();

        SipUser CreateSipUser(string userName, string name = "", string emailAddress = "", string internalCallerIdNumber = "", string externalCallerIdNumber = "");

        void DeleteSipUser(string id);

        SipUser GetSingleSipUser(string id);

        SipUser UpdateSipUser(string id, string name = "", string internalCallerIdNumber = "", string externalCallerIdNumber = "", bool resetPassword = false);

        SipUserStatus ShowSipUserStatus(string id);

        SipUser ResetIpUserStatus(string id);
    }
}