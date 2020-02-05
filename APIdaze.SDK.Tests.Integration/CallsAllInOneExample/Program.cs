using System;
using System.IO;
using APIdaze.SDK;
using APIdaze.SDK.Base;
using APIdaze.SDK.Calls;
using APIdaze.SDK.Messages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CallsAllInOneExample
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

            // initiate API factory
            var apiFactory = ApplicationManager.CreateApiFactory(new Credentials(apiKey, apiSecret));
            var callsApi = apiFactory.CreateCallsApi();

            // call details
            var callerId = "14123456789";
            var origin = "48123456789";
            var destination = "48123456789";

            try
            {
                // place a call
                var callId = callsApi.CreateCall(new PhoneNumber(callerId), origin, destination, CallType.NUMBER);
                Console.WriteLine("Call with id = {0} has been initiated.", callId);

                // get call details
                var call = callsApi.GetCall(callId);
                if (call != null)
                {
                    Console.WriteLine("Initiated call details = {0}", JsonConvert.SerializeObject(call, Formatting.Indented));
                }
                else
                {
                    Console.WriteLine("There is no call with id = {0}", callId);
                }

                // get full list of calls for your domain
                var calls = callsApi.GetCalls();
                calls.ForEach(x => Console.WriteLine("Calls: {0}", JsonConvert.SerializeObject(x)));

                // delete a call by id
                callsApi.DeleteCall(callId);
                Console.WriteLine("Call with id = {0} has been deleted.", callId);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("An error occurred during communicating with API", e);
            }
            catch (CreateCallResponseException e)
            {
                Console.WriteLine("Placing the call failed due to {0}.", e.Message);
            }
            catch (DeleteCallResponseException e)
            {
                Console.WriteLine("Deleting the call failed due to {0}.", e.Message);
            }
            catch (InvalidPhoneNumberException e)
            {
                Console.WriteLine("Phone number {0} is invalid", e.Message);
            }
        }
    }
}
