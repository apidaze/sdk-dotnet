using System;
using System.IO;
using APIdaze.SDK;
using APIdaze.SDK.Base;
using Microsoft.Extensions.Configuration;

namespace UpdateCdrHttpHandlerExample
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

            var handlerId = 111L;
            var handlerName = "Some cool handler2";
            var handlerUrl = "https://www.wp.com";

            try
            {
                // initialize a CdrHttpHandler API
                var cdrHttpHandlersApi = apiFactory.CreateCdrHttpHandlersApi();

                // create CdrHttpHandler
                var result = cdrHttpHandlersApi.UpdateCdrHttpHandler(handlerId, handlerName, new Uri(handlerUrl));
                Console.WriteLine("Updated CdrHttpHandler: {0}", result);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("An error occurred during communicating with API", e);
            }
            catch (UriFormatException e)
            {
                Console.WriteLine("handlerUrl is invalid {0}", e.Message);
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