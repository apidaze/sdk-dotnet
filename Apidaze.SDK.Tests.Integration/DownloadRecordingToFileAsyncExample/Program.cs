using System;
using System.IO;
using System.Threading.Tasks;
using Apidaze.SDK;
using Apidaze.SDK.Base;
using Microsoft.Extensions.Configuration;

namespace DownloadRecordingToFileAsyncExample
{
    class Program
    {
        static async Task Main(string[] args)
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

                var sourceFileName = "example_recording.wav";
                var targetFilePath = Path.GetFullPath(@"foo\");

                Console.WriteLine("Starting downloading the file 1");
                await recordingsApi.DownloadRecordingToFileAsync(sourceFileName, Path.Combine(targetFilePath, "file1.wav"));
                Console.WriteLine("The file 1 has been downloaded.");

                Console.WriteLine("Starting downloading the file 2");
                await recordingsApi.DownloadRecordingToFileAsync(sourceFileName, Path.Combine(targetFilePath, "file2.wav"));
                Console.WriteLine("The file 2 has been downloaded.");

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