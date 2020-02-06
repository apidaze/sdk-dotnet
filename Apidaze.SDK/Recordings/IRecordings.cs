using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Apidaze.SDK.Recordings
{
    public interface IRecordings
    {
        IEnumerable<string> GetRecordingsList();

        Stream DownloadRecording(string sourceFileName);

        Task DownloadRecodingToFileAsync(string sourceFileName, string target);

        FileInfo DownloadRecordingToFile(string sourceFileName, string target);

        void DeleteRecording(string fileName);
    }
}