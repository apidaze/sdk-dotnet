using System;
using System.IO;
using System.Linq;
using Apidaze.SDK;
using Apidaze.SDK.Base;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace RecordingsExample
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

            // initialize API factory
            var apiFactory = ApplicationManager.CreateApiFactory(new Credentials(apiKey, apiSecret));

            try
            {
                // initialize a Recordings API
                var recordingsApi = apiFactory.CreateRecordingsApi();

                // get recordings list
                var list =  recordingsApi.GetRecordingsList();
                list.ToList().ForEach(x => Console.WriteLine("Recordings: {0}", JsonConvert.SerializeObject(x, Formatting.Indented)));
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