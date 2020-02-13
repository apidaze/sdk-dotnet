using System;
using System.IO;
using Apidaze.SDK;
using Apidaze.SDK.Base;
using Microsoft.Extensions.Configuration;

namespace DownloadMediaFile
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

            // initiate ApplicationAction
            var applicationClient = ApplicationManager.CreateApiFactory(new Credentials(apiKey, apiSecret));

            try
            {
                // initialize mediaFilesApi
                var mediaFilesApi = applicationClient.CreateMediaFilesApi();

                // get media files list
                var downloadMediaFile = mediaFilesApi.DownloadMediaFile("zzz8.wav");
                using FileStream fs = new FileStream("zzz8.wav", FileMode.OpenOrCreate);
                downloadMediaFile.CopyTo(fs);
                fs.Flush();

                Console.WriteLine("System environment variables API_KEY and API_SECRET must be set.");
                Console.WriteLine("The file zzz8.wav has been downloaded.");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("An error occurred during communicating with API", e);
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