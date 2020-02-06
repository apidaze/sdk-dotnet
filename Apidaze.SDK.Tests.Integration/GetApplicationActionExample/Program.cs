using Apidaze.SDK;
using Apidaze.SDK.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace GetApplicationActionExample
{
    class Program
    {
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

            // initiate API factory
            var apiFactory = ApplicationManager.CreateApiFactory(new Credentials(apiKey, apiSecret));

            try
            {
                // get application by id
                var id = 3164L;
                var applicationsApi = apiFactory.CreateApplicationsApi();
                var appActionById = applicationsApi.GetApplicationActionById(id);

                Console.WriteLine(
                    appActionById != null
                        ? "Retrieved ApplicationAction for application with id = {0}"
                        : "Application with id = {} not found.", id);

                // get application by api_key
                var subAppApiKey = "";

                var appActionByApiKey = applicationsApi.GetApplicationsByApiKey(subAppApiKey);

                Console.WriteLine(
                    appActionByApiKey != null
                        ? "Retrieved ApplicationAction for application with api_key = {0}"
                        : "Application with id = {0} not found.", subAppApiKey);

                // get application by name
                var name = "APPLICATION 69";

                var appActionByName = applicationsApi.GetApplicationActionByName(name);

                Console.WriteLine(
                    appActionByName != null
                        ? "Retrieved ApplicationAction for application with name = {0}"
                        : "Application with id = {0} not found.", name);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("An error occurred during communicating with API, {0}", e.Message);
            }
        }

        private static IConfigurationRoot BuildConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("client-secrets.json", optional: true, reloadOnChange: true).Build();
        }
    }
}
