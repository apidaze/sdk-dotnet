using System.Collections.Generic;
using System.IO;

namespace Apidaze.SDK.MediaFiles
{
    /// <summary>
    /// Interface IMediaFiles
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
        List<dynamic> GetMediaFilesList(bool details = false, string filter = "", string lastToken = "", int maxItems = 500);

        /// <summary>
        /// Uploads the media file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="mediafile">The mediafile.</param>
        /// <returns>dynamic.</returns>
        dynamic UploadMediaFile(string name, string mediafile);

        /// <summary>
        /// Deletes the media file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>dynamic.</returns>
        dynamic DeleteMediaFile(string fileName);

        /// <summary>
        /// Downloads the media file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>MemoryStream.</returns>
        MemoryStream DownloadMediaFile(string fileName);

        /// <summary>
        /// Shows the media file summary.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>dynamic.</returns>
        dynamic ShowMediaFileSummary(string fileName);
    }
}