using System.Collections.Generic;

namespace Apidaze.SDK.SipUsers
{
    /// <summary>
    ///     Interface ISipUsers
    /// </summary>
    public interface ISipUsers
    {
        /// <summary>
        ///     Gets the sip users.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        List<SipUser> GetSipUsers();

        /// <summary>
        ///     Creates the sip user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="name">The name.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="internalCallerIdNumber">The internal caller identifier number.</param>
        /// <param name="externalCallerIdNumber">The external caller identifier number.</param>
        /// <returns>SipUser.</returns>
        SipUser CreateSipUser(string userName, string name = "", string emailAddress = "",
            string internalCallerIdNumber = "", string externalCallerIdNumber = "");

        /// <summary>
        ///     Deletes the sip user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteSipUser(string id);

        /// <summary>
        ///     Gets the single sip user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>SipUser.</returns>
        SipUser GetSingleSipUser(string id);

        /// <summary>
        ///     Updates the sip user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="internalCallerIdNumber">The internal caller identifier number.</param>
        /// <param name="externalCallerIdNumber">The external caller identifier number.</param>
        /// <param name="resetPassword">if set to <c>true</c> [reset password].</param>
        /// <returns>SipUser.</returns>
        SipUser UpdateSipUser(string id, string name = "", string internalCallerIdNumber = "",
            string externalCallerIdNumber = "", bool resetPassword = false);

        /// <summary>
        ///     Shows the sip user status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>SipUserStatus.</returns>
        SipUserStatus ShowSipUserStatus(string id);

        /// <summary>
        ///     Resets the user password.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>SipUser.</returns>
        SipUser ResetUserPassword(string id);
    }
}