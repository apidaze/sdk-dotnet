using Apidaze.SDK;
using Apidaze.SDK.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CredentialsValidationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("client-secrets.json", optional: true, reloadOnChange: true).Build();
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
                // validates credentials 
                Console.WriteLine(apiFactory.CreateCredentialsValidatorApi().ValidateCredentials()
                    ? "Credentials are valid."
                    : "Credentials are not valid.");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("An error occurred during communicating with API", e);
                throw;
            }
        }
    }
}