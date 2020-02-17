using Apidaze.SDK;
using Apidaze.SDK.Base;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CreateUpdateSipUserExample
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
                // initialize a SIP Users API
                var cdrHttpHandlersApi = apiFactory.CreateSipUsersApi();

                // create SIP user
                var response = cdrHttpHandlersApi.CreateSipUser("testUser2", "test2", "test@test.com", "1412555555", "14125423968");
                Console.WriteLine("New SIP user : {0}", JsonConvert.SerializeObject(response, Formatting.Indented));

                // update SIP user
                var updatedSipUser =
                    cdrHttpHandlersApi.UpdateSipUser(response.Id, "test3", "14125423968", "14125423000", true);
                Console.WriteLine("Update SIP user : {0}", JsonConvert.SerializeObject(updatedSipUser, Formatting.Indented));

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
