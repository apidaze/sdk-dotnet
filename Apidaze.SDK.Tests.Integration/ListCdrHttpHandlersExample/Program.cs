using System;
using System.IO;
using APIdaze.SDK;
using APIdaze.SDK.Base;
using Microsoft.Extensions.Configuration;

namespace ListCdrHttpHandlersExample
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
                // initialize a CdrHttpHandler API
                var cdrHttpHandlersApi = apiFactory.CreateCdrHttpHandlersApi();

                // get CdrHttpHandler list
                var responseList = cdrHttpHandlersApi.GetCdrHttpHandlers();
                Console.WriteLine("CdrHttpHandlers list: {0}", responseList);

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
