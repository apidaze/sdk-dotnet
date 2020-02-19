using System;
using System.IO;
using Apidaze.SDK;
using Apidaze.SDK.Base;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace UpdateExternalScriptUrl
{
    /// <summary>
    /// Class Program.
    /// </summary>
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

            // id of script to be fetched
            var id = 1801L;

            // new url
            var newScriptUrl = "";

            try
            {
                // initialize external scripts api
                var externalScriptsApi = apiFactory.CreateExternalScriptsApi();

                // update external script
                var script = externalScriptsApi.UpdateExternalScriptUrl(id, new Uri(newScriptUrl));
                Console.WriteLine("Updated {0}", JsonConvert.SerializeObject(script, Formatting.Indented));
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("An error occurred during communicating with API, {0}", e.Message);
            }
            catch (UriFormatException e)
            {
                Console.WriteLine("newScriptUrl is invalid {0}", e.Message);
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
