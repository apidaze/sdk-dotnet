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
    public class Recordings : BaseApiClient, IRecordings
    {
        public Recordings(IRestClient client, Credentials credentials) : base(client, credentials)
        {
        }

        protected override string Resource => "/recordings";

        public IEnumerable<string> GetRecordingsList()
        {
            return FindAll<string>();
        }

        public Stream DownloadRecording(string sourceFileName)
        {
            var restRequest = DownloadRequest(sourceFileName);
            var response = Client.DownloadData(restRequest);
            return new MemoryStream(response);
        }

        public async Task<FileInfo> DownloadRecodingToFileAsync(string sourceFileName, string target)
        {
            var restRequest = DownloadRequest(sourceFileName);
            var response = await Client.ExecuteTaskAsync(restRequest);

            if (response.StatusCode != HttpStatusCode.OK) throw new InvalidOperationException(response.ErrorMessage);

            return SaveFileToFolder(sourceFileName, target, response);
        }

        public FileInfo DownloadRecordingToFile(string sourceFileName, string target)
        {
            var restRequest = DownloadRequest(sourceFileName);
            var response = Client.Execute(restRequest);
            if (response.StatusCode != HttpStatusCode.OK) throw new InvalidOperationException(response.ErrorMessage);

            return SaveFileToFolder(sourceFileName, target, response);
        }

        public void DeleteRecording(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("file name must not be null or empty");
            Delete(fileName);
        }

        private RestRequest DownloadRequest(string sourceFileName)
        {
            var restRequest = AuthenticateRequest();
            restRequest.Resource += "/{file_name}";
            restRequest.AddUrlSegment("file_name", sourceFileName);
            return restRequest;
        }

        private static FileInfo SaveFileToFolder(string sourceFileName, string target, IRestResponse response)
        {
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