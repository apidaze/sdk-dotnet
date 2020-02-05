using System;
using System.IO;
using APIdaze.SDK;
using APIdaze.SDK.Base;
using Microsoft.Extensions.Configuration;

namespace DownloadRecordingAsStreamExample
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
                var targetFilePath = @"foo\fileFromStream.wav";

                if (!Directory.Exists(targetFilePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(targetFilePath));
                }
                using var stream = recordingsApi.DownloadRecording(sourceFileName);
                using var fileStream = new FileStream(targetFilePath, FileMode.Create, FileAccess.Write);
                stream.CopyTo(fileStream);
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
