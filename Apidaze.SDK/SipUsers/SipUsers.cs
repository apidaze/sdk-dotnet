using System;
using System.Collections.Generic;
using System.Text;
using Apidaze.SDK.Base;
using RestSharp;

namespace Apidaze.SDK.SipUsers
{
    /// <summary>
    /// Class SipUsers.
    /// Implements the <see cref="Apidaze.SDK.Base.BaseApiClient" />
    /// Implements the <see cref="Apidaze.SDK.SipUsers.ISipUsers" />
    /// </summary>
    /// <seealso cref="Apidaze.SDK.Base.BaseApiClient" />
    /// <seealso cref="Apidaze.SDK.SipUsers.ISipUsers" />
    public class SipUsers : BaseApiClient, ISipUsers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SipUsers"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="credentials">The credentials.</param>
        public SipUsers(IRestClient client, Credentials credentials)
            : base(client, credentials)
        {
        }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <value>The resource.</value>
        protected override string Resource => "/sipusers";

        /// <summary>
        /// Gets the sip users.
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public IEnumerable<string> GetSipUsers()
        {
            return FindAll<string>();
        }

        /// <summary>
        /// Creates the sip user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="name">The name.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="internalCallerIdNumber">The internal caller identifier number.</param>
        /// <param name="externalCallerIdNumber">The external caller identifier number.</param>
        /// <returns>SipUser.</returns>
        /// <exception cref="ArgumentException">userName must not be null or empty</exception>
        public SipUser CreateSipUser(string userName, string name = "", string emailAddress = "", string internalCallerIdNumber = "", string externalCallerIdNumber = "")
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("userName must not be null or empty");
            }

            var requestBody = new Dictionary<string, string>
            {
                { "userName", userName },
                { "name", name },
                { "email_address", emailAddress },
                { "internal_caller_id_number", internalCallerIdNumber },
                { "external_caller_id_number", externalCallerIdNumber },
            };
            return Create<SipUser>(requestBody);
        }

        /// <summary>
        /// Deletes the sip user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteSipUser(string id)
        {
            Delete(id);
        }

        /// <summary>
        /// Gets the single sip user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>SipUser.</returns>
        /// <exception cref="ArgumentException">id must not be null</exception>
        public SipUser GetSingleSipUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("id must not be null");
            }

            return FindById<SipUser>(id);
        }

        /// <summary>
        /// Updates the sip user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="internalCallerIdNumber">The internal caller identifier number.</param>
        /// <param name="externalCallerIdNumber">The external caller identifier number.</param>
        /// <param name="resetPassword">if set to <c>true</c> [reset password].</param>
        /// <returns>SipUser.</returns>
        public SipUser UpdateSipUser(string id, string name = "", string internalCallerIdNumber = "", string externalCallerIdNumber = "", bool resetPassword = false)
        {

            var requestBody = new Dictionary<string, string>
            {
                { "name", name },
                { "internal_caller_id_number", internalCallerIdNumber },
                { "external_caller_id_number", externalCallerIdNumber },
                { "reset_password", resetPassword.ToString() },
            };
            return Update<SipUser>(id, requestBody);
        }

        /// <summary>
        /// Shows the sip user status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>SipUserStatus.</returns>
        /// <exception cref="ArgumentException">id must not be null</exception>
        public SipUserStatus ShowSipUserStatus(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("id must not be null");
            }

            return FindById<SipUserStatus>(id, "status");
        }

        /// <summary>
        /// Resets the ip user status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>SipUser.</returns>
        /// <exception cref="ArgumentException">id must not be null or empty</exception>
        public SipUser ResetIpUserStatus(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("id must not be null or empty");
            }

            var requestBody = new Dictionary<string, string>
            {
                { "id", id },
            };
            return Create<SipUser>(requestBody, "password");
        }
    }
}