using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Apidaze.SDK.Recordings
{
    /// <summary>
    /// Interface IRecordings
    /// </summary>
    public interface IRecordings
    {
        /// <summary>
        /// Gets the recordings list.
        /// </summary>
        /// <returns>IEnumerable&lt;System.String&gt;.</returns>
        IEnumerable<string> GetRecordingsList();

        /// <summary>
        /// Downloads the recording.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <returns>Stream.</returns>
        Stream DownloadRecording(string sourceFileName);

        Task<FileInfo> DownloadRecordingToFileAsync(string sourceFileName, string target);

        /// <summary>
        /// Downloads the recording to file.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="target">The target.</param>
        /// <returns>FileInfo.</returns>
        FileInfo DownloadRecordingToFile(string sourceFileName, string target);

        /// <summary>
        /// Deletes the recording.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        void DeleteRecording(string fileName);
    }
}