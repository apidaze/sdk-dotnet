using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace APIdaze.SDK.Recordings
{
    public interface IRecordings
    {
        IEnumerable<string> GetRecordingsList();

        Stream DownloadRecording(string sourceFileName);

        Task<FileInfo> DownloadRecodingToFileAsync(string sourceFileName, string target);

        FileInfo DownloadRecordingToFile(string sourceFileName, string target);

        void DeleteRecording(string fileName);
    }
}