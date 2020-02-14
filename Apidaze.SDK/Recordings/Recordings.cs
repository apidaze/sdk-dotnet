using Apidaze.SDK.Base;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Apidaze.SDK.Recordings
{
    /// <summary>
    /// Class Recordings.
    /// Implements the <see cref="Apidaze.SDK.Base.BaseApiClient" />
    /// Implements the <see cref="Apidaze.SDK.Recordings.IRecordings" />
    /// </summary>
    /// <seealso cref="Apidaze.SDK.Base.BaseApiClient" />
    /// <seealso cref="Apidaze.SDK.Recordings.IRecordings" />
    public class Recordings : BaseApiClient, IRecordings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Recordings" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="credentials">The credentials.</param>
        public Recordings(IRestClient client, Credentials credentials) : base(client, credentials)
        {
        }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <value>The resource.</value>
        protected override string Resource => "/recordings";

        /// <summary>
        /// Gets the recordings list.
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        public IEnumerable<string> GetRecordingsList()
        {
            return FindAll<string>();
        }

        /// <summary>
        /// Downloads the recording.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <returns>Stream.</returns>
        public Stream DownloadRecording(string sourceFileName)
        {
            var restRequest = DownloadRequest(sourceFileName);
            var response = Client.DownloadData(restRequest);
            return new MemoryStream(response);
        }

        public async Task<FileInfo> DownloadRecordingToFileAsync(string sourceFileName, string target)
        {
            var restRequest = DownloadRequest(sourceFileName);
            var response = await Client.ExecuteTaskAsync(restRequest);
            return SaveFileToFolder(sourceFileName, target, response);
        }

        /// <summary>
        /// Downloads the recording to file.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="target">The target.</param>
        /// <returns>FileInfo.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        public FileInfo DownloadRecordingToFile(string sourceFileName, string target)
        {
            var restRequest = DownloadRequest(sourceFileName);
            var response = Client.Execute(restRequest);
            return SaveFileToFolder(sourceFileName, target, response);
        }

        private static void CheckStatusCode(IRestResponse response)
        {
            if (response.StatusCode != HttpStatusCode.OK) throw new InvalidOperationException(response.ErrorMessage);
        }

        /// <summary>
        /// Deletes the recording.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <exception cref="ArgumentException">file name must not be null or empty</exception>
        /// <exception cref="System.ArgumentException">file name must not be null or empty</exception>
        public void DeleteRecording(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("file name must not be null or empty");
            Delete(fileName);
        }

        /// <summary>
        /// Downloads the request.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <returns>RestRequest.</returns>
        private RestRequest DownloadRequest(string sourceFileName)
        {
            var restRequest = AuthenticateRequest();
            restRequest.Resource += "/{file_name}";
            restRequest.AddUrlSegment("file_name", sourceFileName);
            return restRequest;
        }

        private static FileInfo SaveFileToFolder(string sourceFileName, string target, IRestResponse response)
        {
            CheckStatusCode(response);
            var targetDir = Path.GetDirectoryName(target);
            var fileName = Path.GetFileName(target);
            if (string.IsNullOrEmpty(fileName)) fileName = sourceFileName;

            var dirExists = Directory.Exists(targetDir);
            if (!dirExists)
                Directory.CreateDirectory(targetDir);

            var fullPathName = Path.Combine(targetDir, fileName);
            response.RawBytes.SaveAs(fullPathName);
            return new FileInfo(fullPathName);
        }
    }
}