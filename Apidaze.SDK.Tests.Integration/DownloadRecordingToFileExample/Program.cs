using System;
using System.IO;
using Apidaze.SDK;
using Apidaze.SDK.Base;
using Microsoft.Extensions.Configuration;

namespace DownloadRecordingToFileExample
{
    class Program
    {
        static void Main()
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

                // 1. download a file to local directory without changing the name of the file
                var targetFilePath = Path.GetFullPath(@"foo\");
                var file1 = recordingsApi.DownloadRecordingToFile(sourceFileName, targetFilePath);
                Console.WriteLine("The {0} file has been downloaded to {1}", sourceFileName, file1);

                // 2. download a file to local directory and change the name of target file
                var file2 = recordingsApi.DownloadRecordingToFile(sourceFileName, Path.Combine(targetFilePath, "my-cool-recoding.wav"));
                Console.WriteLine("The {0} file has been downloaded to {1}", sourceFileName, file2);

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
