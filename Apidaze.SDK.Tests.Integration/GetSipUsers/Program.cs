using Apidaze.SDK;
using Apidaze.SDK.Base;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;

namespace GetSipUsersExample
{
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            var config = BuildConfig();
            var apiKey = config["API_KEY"];
            var apiSecret = config["API_SECRET"];

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
            {
                Console.WriteLine("System environment variables API_KEY and API_SECRET must be set.");
                Environment.Exit(0);
            }

            // initialize API factory
            var apiFactory = ApplicationManager.CreateApiFactory(new Credentials(apiKey, apiSecret));

            try
            {
                // initialize a SipUsers API
                var cdrHttpHandlersApi = apiFactory.CreateSipUsersApi();

                // get SIP users list
                var response = cdrHttpHandlersApi.GetSipUsers();
                response.ForEach(x => Console.WriteLine("SIP users list: {0}", JsonConvert.SerializeObject(response, Formatting.Indented)));

                // create SIP user
                var user = cdrHttpHandlersApi.CreateSipUser("testUser10", "test", "test@test.com", "1412555555", "14125423968");
                Console.WriteLine("SIP user : {0}", JsonConvert.SerializeObject(user, Formatting.Indented));

                // get SIP single user 
                var singleSipUser = cdrHttpHandlersApi.GetSingleSipUser(user.Id); 
                Console.WriteLine("Single SIP user: {0}", JsonConvert.SerializeObject(singleSipUser, Formatting.Indented));

                // show SIP user status 
                var status = cdrHttpHandlersApi.ShowSipUserStatus(singleSipUser.Id);
                Console.WriteLine("Status SIP user: {0}", JsonConvert.SerializeObject(status, Formatting.Indented));

                // reset SIP password
                var userWithUpdatedPassword = cdrHttpHandlersApi.ResetUserPassword(singleSipUser.Id);
                Console.WriteLine("User with updated password: {0}", JsonConvert.SerializeObject(userWithUpdatedPassword, Formatting.Indented));

            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("An error occurred during communicating with API, {0}", e.Message);
            }
        }

        /// <summary>
        /// Builds the configuration.
        /// </summary>
        /// <returns>IConfigurationRoot.</returns>
        private static IConfigurationRoot BuildConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("client-secrets.json", optional: true, reloadOnChange: true).Build();
        }
    }
}
