using Apidaze.SDK;
using Apidaze.SDK.Base;
using Apidaze.SDK.Messages;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Apidaze.SDK.Exception;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MessageExample
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
            var apiFactory = ApplicationManager.CreateApiFactory(new Credentials(apiKey, apiSecret), "https://api.apidaze.io/");

            var from = "14125423968";
            var to = "48504916910";
            var messageBody = "Have a nice day!";

            try
            {
                // initialize a message API
                var messageApi = apiFactory.CreateMessageApi();

                // send a text
                var response = messageApi.SendTextMessage(new PhoneNumber(from), new PhoneNumber(to), messageBody);
                Console.WriteLine(JToken.Parse(response).ToString(Formatting.Indented));
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("An error occurred during communicating with API {0}", e.Message);
                throw;
            }
            catch (InvalidPhoneNumberException e)
            {
                Console.WriteLine("Phone number {0} is invalid", e.Message);
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
