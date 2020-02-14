using Apidaze.SDK.Base;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Apidaze.SDK.MediaFiles
{
    /// <summary>
    /// Class MediaFiles.
    /// Implements the <see cref="Apidaze.SDK.Base.BaseApiClient" />
    /// Implements the <see cref="Apidaze.SDK.MediaFiles.IMediaFiles" />
    /// </summary>
    /// <seealso cref="Apidaze.SDK.Base.BaseApiClient" />
    /// <seealso cref="Apidaze.SDK.MediaFiles.IMediaFiles" />
    public class MediaFiles : BaseApiClient, IMediaFiles
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFiles"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="credentials">The credentials.</param>
        public MediaFiles(IRestClient client, Credentials credentials) : base(client, credentials)
        {
        }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <value>The resource.</value>
        protected override string Resource => "/mediafiles";

        /// <summary>
        /// Gets the media files list.
        /// </summary>
        /// <param name="details">if set to <c>true</c> [details].</param>
        /// <param name="filter">The filter.</param>
        /// <param name="lastToken">The last token.</param>
        /// <param name="maxItems">The maximum items.</param>
        /// <returns>List&lt;dynamic&gt;.</returns>
        public List<dynamic> GetMediaFilesList(bool details = false, string filter = "", string lastToken = "", int maxItems = 500)
        {
            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.GET;
            restRequest.AddParameter("details", details);
            restRequest.AddParameter("filter", filter);
            restRequest.AddParameter("last_token", lastToken);
            restRequest.AddParameter("max_items", maxItems);

            var response = Client.Execute(restRequest);
            EnsureSuccessResponse(response);

            return JsonConvert.DeserializeObject<IEnumerable<dynamic>>(response.Content).ToList();
        }

        /// <summary>
        /// Downloads the media file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>MemoryStream.</returns>
        /// <exception cref="ArgumentException">fileName must not be null or empty</exception>
        public MemoryStream DownloadMediaFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("fileName must not be null or empty");

            var restRequest = AuthenticateRequest();
            restRequest.Method = Method.GET;
            restRequest.AddHeader("Content-Type", "audio/wav");
            restRequest.Resource += "/{filename}";
            restRequest.AddUrlSegment("filename", fileName);

            var response = Client.DownloadData(restRequest);
            return new MemoryStream(response);
        }
    }
}
