using System;
using System.IO;
using Apidaze.SDK;
using Apidaze.SDK.Base;
using Microsoft.Extensions.Configuration;

namespace PostDeleteDownloadMediaFile
{
    /// <summary>
    /// Class Program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
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

            // initiate ApplicationAction
            var applicationClient = ApplicationManager.CreateApiFactory(new Credentials(apiKey, apiSecret));

            try
            {
                // initialize mediaFilesApi
                var mediaFilesApi = applicationClient.CreateMediaFilesApi();

                //upload media file
                var uploadedMediaFile = mediaFilesApi.UploadMediaFile("testFile", "fullPath");
                if (uploadedMediaFile.Status != null)
                    Console.WriteLine("The file has been uploaded.");

                // download media file
                var downloadMediaFile = mediaFilesApi.DownloadMediaFile("test.wav");
                using FileStream fs = new FileStream("test.wav", FileMode.OpenOrCreate);
                using var ms = new MemoryStream(downloadMediaFile);
                ms.CopyTo(fs);
                fs.Flush();

                Console.WriteLine("The file test.wav has been downloaded.");

                // delete media file 
                mediaFilesApi.DeleteMediaFile("test.wav");
                Console.WriteLine("The file has been deleted.");

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