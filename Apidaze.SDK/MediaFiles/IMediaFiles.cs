using Apidaze.SDK.Common;
using System.Collections.Generic;

namespace Apidaze.SDK.MediaFiles
{
    /// <summary>
    /// Interface IMediaFiles.
    /// </summary>
    public interface IMediaFiles
    {
        /// <summary>
        /// Gets the media files list.
        /// </summary>
        /// <param name="details">if set to <c>true</c> [details].</param>
        /// <param name="filter">The filter.</param>
        /// <param name="lastToken">The last token.</param>
        /// <param name="maxItems">The maximum items.</param>
        /// <returns>List&lt;dynamic&gt;.</returns>
        List<MediaFile> GetMediaFilesList(bool details = false, string filter = "", string lastToken = "", int maxItems = 500);

        /// <summary>
        /// Uploads the media file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="mediaFile">The media file.</param>
        /// <returns>dynamic.</returns>
        Response UploadMediaFile(string name, string mediaFile);

        /// <summary>
        /// Deletes the media file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        void DeleteMediaFile(string fileName);

        /// <summary>
        /// Downloads the media file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>MemoryStream.</returns>
        byte[] DownloadMediaFile(string fileName);

        /// <summary>
        /// Shows the media file summary.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>dynamic.</returns>
        ResponseHeader ShowMediaFileSummary(string fileName);
    }
}