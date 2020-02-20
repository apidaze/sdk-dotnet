using System;
using System.Collections.Generic;
using System.Linq;
using Apidaze.SDK.Base;
using Apidaze.SDK.Common;
using Newtonsoft.Json;
using RestSharp;

namespace Apidaze.SDK.MediaFiles
{
    /// <summary>
    ///     Class MediaFiles.
    ///     Implements the <see cref="Apidaze.SDK.Base.BaseApiClient" />
    ///     Implements the <see cref="Apidaze.SDK.MediaFiles.IMediaFiles" />
    /// </summary>
    /// <seealso cref="Apidaze.SDK.Base.BaseApiClient" />
    /// <seealso cref="Apidaze.SDK.MediaFiles.IMediaFiles" />
    public class MediaFiles : BaseApiClient, IMediaFiles
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MediaFiles" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="credentials">The credentials.</param>
        public MediaFiles(IRestClient client, Credentials credentials)
            : base(client, credentials)
        {
        }

        /// <summary>
        ///     Gets the resource.
        /// </summary>
        /// <value>The resource.</value>
        protected override string Resource => "/mediafiles";

        /// <summary>
        ///     Gets the media files list.
        /// </summary>
        /// <param name="details">if set to <c>true</c> [details].</param>
        /// <param name="filter">The filter.</param>
        /// <param name="lastToken">The last token.</param>
        /// <param name="maxItems">The maximum items.</param>
        /// <returns>List&lt;dynamic&gt;.</returns>
        public List<MediaFile> GetMediaFilesList(bool details = false, string filter = "", string lastToken = "",
            int maxItems = 500)
        {
            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.GET;
            restRequest.AddParameter("details", details);
            restRequest.AddParameter("filter", filter);
            restRequest.AddParameter("last_token", lastToken);
            restRequest.AddParameter("max_items", maxItems);

            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);

            return JsonConvert.DeserializeObject<IEnumerable<MediaFile>>(response.Content).ToList();
        }

        /// <summary>
        ///     Uploads the media file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="mediaFile">The media file.</param>
        /// <returns>dynamic.</returns>
        public Response UploadMediaFile(string name, string mediaFile)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("name must not be null or empty");

            if (string.IsNullOrEmpty(mediaFile)) throw new ArgumentException("mediaFile must not be null or empty");

            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.POST;
            restRequest.AddHeader("Content-Type", "multipart/form-data");

            restRequest.AddFile("mediafile", mediaFile, "audio/wav");
            restRequest.AddParameter("name", name);
            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);
            return JsonConvert.DeserializeObject<Response>(response.Content);
        }

        public void DeleteMediaFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("fileName must not be null or empty");

            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.DELETE;
            restRequest.Resource += "/{fileName}";
            restRequest.AddUrlSegment("fileName", fileName);
            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);
        }

        /// <summary>
        ///     Downloads the media file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>MemoryStream.</returns>
        /// <exception cref="ArgumentException">fileName must not be null or empty</exception>
        public byte[] DownloadMediaFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("fileName must not be null or empty");

            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.GET;
            restRequest.AddHeader("Content-Type", "audio/wav");
            restRequest.Resource += "/{filename}";
            restRequest.AddUrlSegment("filename", fileName);

            return Client.DownloadData(restRequest);
        }

        /// <summary>
        ///     Shows the media file summary.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>dynamic.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public ResponseHeader ShowMediaFileSummary(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("fileName must not be null or empty");

            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.HEAD;
            restRequest.Resource += "/{fileName}";
            restRequest.AddUrlSegment("fileName", fileName);

            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);
            var headers = response.Headers.ToList();
            return new ResponseHeader
            {
                Connection = headers.Find(p => p.Name == "Connection")?.Value?.ToString(),
                ContentLength = response.ContentLength, ContentType = response.ContentType,
                Date = headers.Find(p => p.Name == "Date")?.Value?.ToString()
            };
        }
    }
}