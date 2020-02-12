using System;
using System.IO;
using Apidaze.SDK;
using Apidaze.SDK.Base;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

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

                if (appActionById != null)
                {
                    try
                    {
                        var scripts = appActionById.CreateExternalScriptsApi().GetExternalScripts();
                        Console.WriteLine("ExternalScripts list: {0}", JsonConvert.SerializeObject(scripts, Formatting.Indented));
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine("An error occurred during communicating with API, {0}", e.Message);
                    }
                }

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
