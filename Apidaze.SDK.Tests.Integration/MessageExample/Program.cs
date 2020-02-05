using APIdaze.SDK;
using APIdaze.SDK.Base;
using APIdaze.SDK.Messages;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using APIdaze.SDK.Exception;

namespace MessageExample
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

            var from = "14125423968";
            var to = "4850916910";
            var messageBody = "Have a nice day!";

            try
            {
                // initialize a message API
                var messageApi = apiFactory.CreateMessageApi();

                // send a text
                var response = messageApi.SendTextMessage(new PhoneNumber(from), new PhoneNumber(to), messageBody);
                Console.WriteLine(response);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("An error occurred during communicating with API", e);
                throw;
            }
            catch (InvalidPhoneNumberException e)
            {
                Console.WriteLine("Phone number {0} is invalid", e.Message);
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
