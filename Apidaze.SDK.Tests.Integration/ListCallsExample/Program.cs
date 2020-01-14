using APIdaze.SDK;
using APIdaze.SDK.Base;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;


namespace ListCallsExample
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
                // initialize calls API
                var callsApi = apiFactory.CreateCallsApi();

                // make request to calls API
                var response =  callsApi.GetCalls();
                response.ForEach(x => Console.WriteLine("Calls: {0}", JsonConvert.SerializeObject(x)));
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("An error occurred during communicating with API", e);
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
